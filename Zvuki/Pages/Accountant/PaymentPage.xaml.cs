using Microsoft.EntityFrameworkCore;
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

namespace Zvuki.Pages.Accountant
{
    /// <summary>
    /// Логика взаимодействия для PaymentPage.xaml
    /// </summary>
    public partial class PaymentPage : Page
    {
        ArrayList listEmployees = new ArrayList();
        ObservableCollection<PaymentAccount> paymentAccounts = new ObservableCollection<PaymentAccount>();

        public PaymentPage()
        {
            InitializeComponent();
            loadData();
            cmbEmployees.ItemsSource = listEmployees;
            cmbEmployees.SelectedIndex = 0;
            PaymentList.ItemsSource = paymentAccounts;
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            CreatePayment();
        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            UpdatePayment();
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            DaletePayment();
        }

        public async void CreatePayment()
        {
            await Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        Employee e = cmbEmployees.SelectedItem as Employee;

                        PaymentAccount paymentAccount = new PaymentAccount
                        {
                            DatePayment = DateTime.Now,
                            SumPayment = Convert.ToInt32(txtSumPayment.Text),
                            Employee = db.Employees.FirstOrDefault(x => x.IdEmployee == e.IdEmployee)
                        };
                        if (MainWindow.validData(paymentAccount))
                        {
                            db.PaymentAccounts.Add(paymentAccount);
                            db.SaveChanges();
                            loadData();
                        }


                       
                      
                    });
                }
            });
        }

        public async void UpdatePayment()
        {
            await Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        PaymentAccount p = paymentAccounts[PaymentList.SelectedIndex];
                        PaymentAccount paymentAccount = db.PaymentAccounts.FirstOrDefault(x => x.IdPaymentAccount == p.IdPaymentAccount);
                        Employee e = cmbEmployees.SelectedItem as Employee;

                        paymentAccount.Employee = db.Employees.FirstOrDefault(x => x.IdEmployee == e.IdEmployee);
                        paymentAccount.SumPayment = Convert.ToInt32(txtSumPayment.Text);

                        if (MainWindow.validData(paymentAccount))
                        {
                            db.SaveChanges();
                            loadData();
                        }
                    });
                }
            });
        }

        public async void DaletePayment()
        {
            await Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        PaymentAccount p = paymentAccounts[PaymentList.SelectedIndex];
                        PaymentAccount paymentAccount = db.PaymentAccounts.FirstOrDefault(x => x.IdPaymentAccount == p.IdPaymentAccount);
                        db.PaymentAccounts.Remove(paymentAccount);
                        db.SaveChanges();
                        loadData();
                    });
                }
            });
        }

        public async void loadData()
        {
            await Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var employees = db.Employees
                    .Include(e => e.Human)
                    .ToList();
                    var paymentAccounts = db.PaymentAccounts
                    .Include(e => e.Employee)
                    .ThenInclude(h => h.Human)
                    .ToList();

                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        this.listEmployees.Clear();
                        this.paymentAccounts.Clear();

                        foreach (var employee in employees)
                        {
                            listEmployees.Add(employee);
                        }

                        foreach (var payment in paymentAccounts)
                        {
                            this.paymentAccounts.Add(payment);
                        }
                    });
                }
            });
        }

        private void PaymentList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                PaymentAccount p = paymentAccounts[PaymentList.SelectedIndex];
                cmbEmployees.SelectedItem = p.Employee;
                txtSumPayment.Text = p.SumPayment.ToString();
            }
            catch (Exception ex)
            {

            }
           

        }
    }
}
