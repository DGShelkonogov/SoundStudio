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

namespace Zvuki.Pages.Register
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
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

                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        foreach (var human in humans)
                        {
                            if (human.Login.Equals(txtLogin.Text) && human.Password.Equals(txtPassword.Text))
                            {
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
