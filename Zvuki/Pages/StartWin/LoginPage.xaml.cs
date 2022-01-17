using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

namespace Zvuki.Pages.Register
{
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
          
   
        }
        private void Button_Click_Register(object sender, RoutedEventArgs e)
        {
            StartWindow win = (StartWindow) Window.GetWindow(this);
            win.setRegister();
        }

        private void Button_Click_Login(object sender, RoutedEventArgs e)
        {
            Login();
        }


        public async void Login()
        {
            await Task.Run(() => {
                using (ApplicationContext db = new ApplicationContext())
                {
                    // получаем объекты из бд
                    var humans = db.Humans.ToList();
                    var clients = db.Clients
                    .Include(x => x.Human)
                    .ToList();
                    var employees = db.Employees
                    .Include(x => x.Human)
                    .ToList();

                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        foreach (var human in humans)
                        {
                            if (human.Login.Equals(txtLogin.Text) && human.Password.Equals(txtPassword.Password))
                            {
                                foreach (var client in clients)
                                {
                                    if (client.Human.Login.Equals(human.Login))
                                    {
                                        DataLoader.saveClient(client);
                                    }
                                }
                                foreach (var employee in employees)
                                {
                                    if (employee.Human.Login.Equals(human.Login))
                                    {
                                        DataLoader.saveEmployee(employee);
                                    }
                                }

                                DataLoader.saveHuman(human);


                                MainWindow window = new MainWindow();
                                window.Show();
                                StartWindow win = (StartWindow)Window.GetWindow(this);
                                win.Close();
                            }
                        }
                    });
                }
            });
        }
    }
}
