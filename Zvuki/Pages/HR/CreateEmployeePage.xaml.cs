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
    public partial class CreateEmployeePage : Page
    {
        bool addEmployee = true, updateEmployee = true;
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

   
            loadData();
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            if (addEmployee)
            {
                //addEmployee = false;
                if (clearForm())
                    blockBtnCreate();
            }
            else
            {
               
                CreateEmployee();
            }

        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            if (updateEmployee)
            {
                //updateEmployee = false;
                if (fillTextBoxs())
                {
                    blockBtnUpdate();
                }
            }
            else
            {
                
                UpdateEmployee();
            }
        }
        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            DeleteEmployee();
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            addEmployee = true;
            updateEmployee = true;

            btnAdd.IsEnabled = true;
            btnUpdate.IsEnabled = true;
            btnDelete.IsEnabled = true;

            positions.Clear();
            clearForm();
            createEmployeeGrid.Visibility = Visibility.Hidden;
            EmployeeList.Visibility = Visibility.Visible;
        }

        public async void CreateEmployee()
        {
            await Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    try
                    {
                        App.Current.Dispatcher.Invoke((Action)delegate
                        {
                            if (txtPassword.Password.Equals(txtRepeatPassword.Password))
                            {
                                if ((DateTime)dpDateOfBirth.SelectedDate < DateTime.Now)
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
                                            Password = txtPassword.Password,
                                            DateOfBirth = (DateTime)dpDateOfBirth.SelectedDate,
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

                                    foreach (Position position in positions)
                                    {
                                        Position p = db.Positions
                                        .FirstOrDefault(x => x.IdPosition == position.IdPosition);
                                        employee.Positions.Add(p);
                                    }
                                    if (MainWindow.validData(employee) &&
                                        MainWindow.validData(mainHuman) &&
                                        MainWindow.validData(account)
                                    )
                                    {
                                        db.Employees.Add(employee);
                                        db.SaveChanges();
                                        loadData();

                                        btnAdd.IsEnabled = true;
                                        btnUpdate.IsEnabled = true;
                                        btnDelete.IsEnabled = true;


                                        addEmployee = true;

                                        createEmployeeGrid.Visibility = Visibility.Hidden;
                                        EmployeeList.Visibility = Visibility.Visible;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("invalid date of birth");

                                }
                            }
                            else
                            {
                                MessageBox.Show("Password doesn't match");
                            }
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                   
                }
            });

        }

        public async void UpdateEmployee()
        {
            await Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    try
                    {
                        App.Current.Dispatcher.Invoke((Action)delegate
                        {
                            if (txtPassword.Password.Equals(txtRepeatPassword.Password))
                            {
                                if ((DateTime)dpDateOfBirth.SelectedDate < DateTime.Now)
                                {
                                    Employee e = EmployeeList.SelectedItem as Employee;
                                    Employee employee = db.Employees
                                    .Include(x => x.Human)
                                    .Include(x => x.BankAccount)
                                    .Include(x => x.Positions)
                                    .FirstOrDefault(x => x.IdEmployee == e.IdEmployee);


                                    employee.Human.Name = txtName.Text;
                                    employee.Human.Surname = txtSurname.Text;
                                    employee.Human.Patronomic = txtPatronymic.Text;
                                    employee.Human.Phone = txtPhone.Text;
                                    employee.Human.Password = txtPassword.Password;
                                    employee.Human.DateOfBirth = (DateTime)dpDateOfBirth.SelectedDate;
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
                                        Position p = db.Positions
                                        .FirstOrDefault(x => x.IdPosition == position.IdPosition);
                                        employee.Positions.Add(p);
                                    }

                                    if (MainWindow.validData(employee) &&
                                        MainWindow.validData(employee.Human) &&
                                        MainWindow.validData(employee.BankAccount)
                                    )
                                    {
                                        updateEmployee = true;

                                        btnAdd.IsEnabled = true;
                                        btnUpdate.IsEnabled = true;
                                        btnDelete.IsEnabled = true;


                                        createEmployeeGrid.Visibility = Visibility.Hidden;
                                        EmployeeList.Visibility = Visibility.Visible;

                                        db.SaveChanges();
                                        loadData();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("invalid date of birth");
                                }
                               
                            }
                            else
                            {
                                MessageBox.Show("Password doesn't match");
                            }
                           
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                   
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
                    try
                    {
                        var positions = db.Positions.ToList();
                        var employees = db.Employees
                        .Include(x => x.Human)
                        .Include(x => x.BankAccount)
                        .Include(x => x.Positions)
                        .ToList();

                        App.Current.Dispatcher.Invoke((Action)delegate
                        {
                            this.employees.Clear();
                            this.listPositions.Clear();
                            cmbPositions.SelectedItem = null;

                            foreach (var position in positions)
                            {
                                this.listPositions.Add(position);
                            }

                            foreach (var employee in employees)
                            {
                                this.employees.Add(employee);
                            }
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                   
                }
            });

        }

        private void cmbPosition_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Position pos = cmbPositions.SelectedItem as Position;
                if(pos != null)
                {
                    if (positions.Contains(pos))
                    {
                        positions.Remove(pos);
                    }
                    else
                    {
                        positions.Add(pos);
                    }
                }
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public bool fillTextBoxs()
        {
            try
            {
                Employee employee = EmployeeList.SelectedItem as Employee;
                txtName.Text = employee.Human.Name;
                txtSurname.Text = employee.Human.Surname;
                txtPatronymic.Text = employee.Human.Patronomic;
                txtPhone.Text = employee.Human.Phone;
                txtPassword.Password = employee.Human.Password;
                dpDateOfBirth.SelectedDate = employee.Human.DateOfBirth;
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
                createEmployeeGrid.Visibility = Visibility.Visible;
                EmployeeList.Visibility = Visibility.Hidden;
                updateEmployee = false;
                return true;
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public bool clearForm()
        {
            try
            {
                cmbPositions.SelectedItem = null;

                txtName.Text = "";
                txtSurname.Text = "";
                txtPatronymic.Text = "";
                txtPhone.Text = "";
                txtPassword.Password = "";
                txtRepeatPassword.Password = "";
                dpDateOfBirth.DisplayDate = DateTime.Now;
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
                createEmployeeGrid.Visibility = Visibility.Visible;
                EmployeeList.Visibility = Visibility.Hidden;
                addEmployee = false;
                return true;


            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public void blockBtnCreate()
        {
            btnAdd.IsEnabled = true;
            btnUpdate.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }

        public void blockBtnUpdate()
        {
            btnAdd.IsEnabled = false;
            btnUpdate.IsEnabled = true;
            btnDelete.IsEnabled = false;
        }
    }
}
