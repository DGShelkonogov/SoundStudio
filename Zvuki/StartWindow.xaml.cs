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
using System.Windows.Shapes;
using Zvuki.Models;

namespace Zvuki
{
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();

            Human human = DataLoader.getHuman();
            if (human != null)
            {
                MainWindow window = new MainWindow();
                window.Show();
                this.Close();
            }

            mainFrame.Source = new Uri("Pages/StartWin/LoginPage.xaml", UriKind.Relative);
        }

        public void setLogin()
        {
            this.Height = 300;
            this.Width = 250;
            mainFrame.Source = new Uri("Pages/StartWin/LoginPage.xaml", UriKind.Relative);
        }
        public void setRegister()
        {

            this.Height = 450;
            this.Width = 400;
            mainFrame.Source = new Uri("Pages/StartWin/RegisterPage.xaml", UriKind.Relative);
        }
    }
}
