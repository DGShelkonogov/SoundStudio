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

namespace Zvuki.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            StartWindow win = (StartWindow)Window.GetWindow(this);
            win.setLogin();
        }

        private void Button_Click_CreateAccount(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Text.Equals(txtRepeatPassword.Text))
            {
                CreateAccount();
            }
        }

        public async void CreateAccount()
        {
            await Task.Run(() => {
                using (ApplicationContext db = new ApplicationContext())
                {
                    try
                    {

                        App.Current.Dispatcher.Invoke((Action)delegate
                        {
                            Human human = new Human
                            {
                                Name = txtName.Text,
                                Surname = txtSurname.Text,
                                Patronomic = txtPatronomic.Text,
                                Phone = txtPhone.Text,
                                Password = txtPassword.Text,
                                DateOfBirth = dpDateOfBirth.DisplayDate,
                                Email = txtEmail.Text,
                                Login = txtLogin.Text,
                                isAdmin = false
                            };

                            Client client = new Client
                            {
                                AmountMoney = 0,
                                Human = human,
                                AudioRecordingClients = new List<AudioRecordingClient>()
                            };

                            if (MainWindow.validData(human) && MainWindow.validData(client))
                            {
                                db.Clients.Add(client);
                                db.SaveChanges();

                                DataLoader.saveHuman(human);
                                DataLoader.saveClient(client);

                                MainWindow window = new MainWindow();
                                window.Show();
                                StartWindow win = (StartWindow)Window.GetWindow(this);
                                win.Close();
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
    }
}

