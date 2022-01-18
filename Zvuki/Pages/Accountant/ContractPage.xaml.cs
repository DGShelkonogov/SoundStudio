using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Zvuki.Models;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;
using Annotation = System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using NPOI.SS.Formula.Functions;

namespace Zvuki.Pages.Accountant
{
    public partial class ContractPage : Page
    {
        ArrayList list = new ArrayList();
        ObservableCollection<Contract> contracts= new ObservableCollection<Contract>();

        string pathToFile = "";
        public ContractPage()
        {
            InitializeComponent();
            cmbEmployees.ItemsSource = list;
            cmbEmployees.SelectedIndex = 0;
            ContractList.ItemsSource = contracts;
           loadData();
        }

        private void Button_Click_Upload(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog(); 
            openFileDialog.Filter = "Docx files (*.docx)|*.docx|Text files (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == true)
            {
                pathToFile = openFileDialog.FileName;
                txtFilePath.Content = "File: " + pathToFile;
            }
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            CreateContract();
        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            UpdateContract();
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            DaleteContract();
        }

        public async void loadData()
        {
            await Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    // получаем объекты из бд
                    var employees = db.Employees
                    .Include(x => x.Human).
                    ToList();

                    var contracts = db.Contracts
                    .Include(x => x.Employee)
                    .ThenInclude(h => h.Human)
                    .ToList();

                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        this.contracts.Clear();
                        list.Clear();

                        foreach (var employee in employees)
                        {
                            list.Add(employee);
                        }

                        foreach (var contract in contracts)
                        {
                            this.contracts.Add(contract);
                        }
                    });
                }
            });
        }

        public async void CreateContract()
        {
            await Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        Employee e = cmbEmployees.SelectedItem as Employee;

                        Contract contract = new Contract
                        {
                            Employee = db.Employees.FirstOrDefault(x => x.IdEmployee == e.IdEmployee),
                            Path = pathToFile
                        };

                        if (MainWindow.validData(contract))
                        {
                            // добавляем их в бд
                            db.Contracts.Add(contract);
                            db.SaveChanges();
                            loadData();
                        }


                       
                    });
                }
            });
        }

        public async void UpdateContract()
        {
            await Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        Contract c = contracts[ContractList.SelectedIndex];
                        Contract contract = db.Contracts.FirstOrDefault(x => x.IdContract == c.IdContract);
                        Employee e = cmbEmployees.SelectedItem as Employee;

                        contract.Employee = db.Employees.FirstOrDefault(x => x.IdEmployee == e.IdEmployee);
                        contract.Path = pathToFile;

                        if (MainWindow.validData(contract))
                        {
                            db.SaveChanges();
                            loadData();
                        }
                    });
                }
            });
        }

        public async void DaleteContract()
        {
            await Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        Contract c = contracts[ContractList.SelectedIndex];
                        Contract contract = db.Contracts.FirstOrDefault(x => x.IdContract == c.IdContract);
                        db.Contracts.Remove(contract);
                        db.SaveChanges();
                        loadData();
                    });
                }
            });
        }


        private void ContractList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Contract c = contracts[ContractList.SelectedIndex];
                txtFilePath.Content = c.Path;
                pathToFile = c.Path;
                cmbEmployees.SelectedItem = c.Employee;

            }
            catch (Exception ex) { }
        }

       
    }
}
