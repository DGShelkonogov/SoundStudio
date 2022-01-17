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

namespace Zvuki.Pages.HR
{
    /// <summary>
    /// Логика взаимодействия для CreateEmployeePage.xaml
    /// </summary>
    public partial class CreateEmployeePage : Page
    {
        bool addEmployee = false, updateEmployee = false;
        ObservableCollection<Position> positions = new ObservableCollection<Position>();
        ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
        ArrayList listPositions = new ArrayList();


        public CreateEmployeePage()
        {
            InitializeComponent();
            cmbPositions.ItemsSource = listPositions;
            cmbPositions.SelectedIndex = 0;
            
            PositionList.ItemsSource = positions;
            EmployeeList.ItemsSource= employees;

            cmbPositions.DisplayMemberPath = "Title";
            loadData();
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            addEmployee = !addEmployee;
            if (addEmployee)
            {
                clearTextBoxs();
                createEmployeeGrid.Visibility = Visibility.Visible;
                EmployeeList.Visibility = Visibility.Hidden;
            }
            else
            {
                CreateEmployee();
                createEmployeeGrid.Visibility = Visibility.Hidden;
                EmployeeList.Visibility = Visibility.Visible;
            }

        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            updateEmployee = !updateEmployee;
            if (updateEmployee)
            {
                fillTextBoxs();
                createEmployeeGrid.Visibility = Visibility.Visible;
                EmployeeList.Visibility = Visibility.Hidden;
            }
            else
            {
                UpdateEmployee();
                createEmployeeGrid.Visibility = Visibility.Hidden;
                EmployeeList.Visibility = Visibility.Visible;
            }
        }
        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            DeleteEmployee();
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            addEmployee = false;
            updateEmployee = false;
            createEmployeeGrid.Visibility = Visibility.Hidden;
            EmployeeList.Visibility = Visibility.Visible;
        }

        public async void CreateEmployee()
        {
            await Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        bool isNewLogin = true;
                        Human mainHuman = new Human();
                        var humans = db.Humans.ToList();
                        foreach (var human in humans)
                        {
                            if (human.Login.Equals(txtLogin.Text))
                            {
                                isNewLogin = false;
                                mainHuman = human;
                                break;
                            }
                        }

                        if (isNewLogin)
                        {
                            mainHuman = new Human
                            {
                                Name = txtName.Text,
                                Surname = txtSurname.Text,
                                Patronomic = txtPatronymic.Text,
                                Phone = txtPhone.Text,
                                Password = txtPassword.Text,
                                DateOfBirth = dpDateOfBirth.DisplayDate,
                                Email = txtEmail.Text,
                                Login = txtLogin.Text,
                                isAdmin = false
                            };
                        }

                        BankAccount account = new BankAccount
                        {
                            Bank = txtBankName.Text,
                            Bik = txtBIC.Text,
                            INN = txtTINOfTheBank.Text,
                            KorAccount = txtCorrespondentAccount.Text,
                            KPP = txtCOR.Text,
                            Number = txtNumber.Text
                        };

                        Employee employee = new Employee
                        {
                            Human = mainHuman,
                            BankAccount = account,
                            INN = txtTIN.Text,
                            SNILS = txtSNILS.Text,
                            Positions = new List<Position>()
                        };

                        foreach(Position position in positions)
                        {
                            Position p = db.Positions.FirstOrDefault(x => x.IdPosition == position.IdPosition);
                            employee.Positions.Add(p);
                        }

                        // добавляем их в бд
                        db.Employees.Add(employee);
                        db.SaveChanges();
                        loadData();

                    });
                }
            });

        }

        public async void UpdateEmployee()
        {
            await Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        Employee e = employees[EmployeeList.SelectedIndex];
                        Employee employee = db.Employees.FirstOrDefault(x => x.IdEmployee == e.IdEmployee);

                        db.Entry(employee)
                           .Reference(c => c.Human)
                           .Load();

                        db.Entry(employee)
                     .Reference(c => c.BankAccount)
                     .Load();

                        db.Entry(employee)
                     .Collection(c => c.Positions)
                     .Load();


                        employee.Human.Name = txtName.Text;
                        employee.Human.Surname = txtSurname.Text;
                        employee.Human.Patronomic = txtPatronymic.Text;
                        employee.Human.Phone = txtPhone.Text;
                        employee.Human.Password = txtPassword.Text;
                        employee.Human.DateOfBirth = dpDateOfBirth.DisplayDate;
                        employee.Human.Email = txtEmail.Text;
                        employee.Human.Login = txtLogin.Text;
                        employee.BankAccount.Bank = txtBankName.Text;
                        employee.BankAccount.Bik = txtBIC.Text;
                        employee.BankAccount.INN = txtTINOfTheBank.Text;
                        employee.BankAccount.KorAccount = txtCorrespondentAccount.Text;
                        employee.BankAccount.KPP = txtCOR.Text;
                        employee.BankAccount.Number = txtNumber.Text;
                        employee.INN = txtTIN.Text;
                        employee.SNILS = txtSNILS.Text;
                        employee.Positions.Clear();

                        foreach (Position position in positions)
                        {
                            Position p = db.Positions.FirstOrDefault(x => x.IdPosition == position.IdPosition);
                            employee.Positions.Add(p);
                        }

                        db.SaveChanges();
                        loadData();
                    });
                }
            });
        }

        public async void DeleteEmployee()
        {
            await Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        Employee e = employees[EmployeeList.SelectedIndex];
                        Employee employee = db.Employees
                        .Include(x => x.Human)
                        .Include(x => x.BankAccount)
                        .Include(x => x.Positions)
                        .FirstOrDefault(x => x.IdEmployee == e.IdEmployee);

                        db.Employees.Remove(employee);

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
                    var positions = db.Positions.ToList();
                    var employees = db.Employees.ToList();

                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        this.employees.Clear();
                        foreach (var position in positions)
                        {
                            listPositions.Add(position);
                        }

                        foreach (var employee in employees)
                        {
                            db.Entry(employee)
                           .Reference(c => c.Human)
                           .Load();

                            db.Entry(employee)
                         .Reference(c => c.BankAccount)
                         .Load();

                            db.Entry(employee)
                         .Collection(c => c.Positions)
                         .Load();

                            this.employees.Add(employee);
                        }
                    });
                }
            });

        }

        private void cmbPosition_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Position pos = cmbPositions.SelectedItem as Position;

            if (positions.Contains(pos))
            {
                positions.Remove(pos);
            }
            else
            {
                positions.Add(pos);
            }
        }

        public void fillTextBoxs()
        {
            try
            {
                Employee employee = EmployeeList.SelectedItem as Employee;
                txtName.Text = employee.Human.Name;
                txtSurname.Text = employee.Human.Surname;
                txtPatronymic.Text = employee.Human.Patronomic;
                txtPhone.Text = employee.Human.Phone;
                txtPassword.Text = employee.Human.Password;
                dpDateOfBirth.DisplayDate = employee.Human.DateOfBirth;
                txtEmail.Text = employee.Human.Email;
                txtLogin.Text = employee.Human.Login;
                txtBankName.Text = employee.BankAccount.Bank;
                txtBIC.Text = employee.BankAccount.Bik;
                txtTINOfTheBank.Text = employee.BankAccount.INN;
                txtCorrespondentAccount.Text = employee.BankAccount.KorAccount;
                txtCOR.Text = employee.BankAccount.KPP;
                txtNumber.Text = employee.BankAccount.Number;
                txtTIN.Text = employee.INN;
                txtSNILS.Text = employee.SNILS;
  
                positions.Clear();
                foreach (Position position in employee.Positions)
                {
                    positions.Add(position);
                }


            }
            catch (Exception ex)
            {

            }
        }

        public void clearTextBoxs()
        {
            try
            {
                txtName.Text = "";
                txtSurname.Text = "";
                txtPatronymic.Text = "";
                txtPhone.Text = "";
                txtPassword.Text = "";
                dpDateOfBirth.DisplayDate = new DateTime();
                txtEmail.Text = "";
                txtLogin.Text = "";
                txtBankName.Text = "";
                txtBIC.Text = "";
                txtTINOfTheBank.Text = "";
                txtCorrespondentAccount.Text = "";
                txtCOR.Text = "";
                txtNumber.Text = "";
                txtTIN.Text = "";
                txtSNILS.Text = "";
                PositionList.ItemsSource = "";

            }
            catch (Exception ex)
            {

            }
        }
    }
}
