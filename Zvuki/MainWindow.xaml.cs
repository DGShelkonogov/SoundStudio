using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace Zvuki
{
    public partial class MainWindow : Window
    {

        ArrayList list = new ArrayList() { "Accountant", "Advertiser", "ClientPages", "HR", "Manager", "Сleaner" };
        Grid[] grids = new Grid[6];

        public MainWindow()
        {
            InitializeComponent();
            grids[0] = GridAccountant;
            grids[1] = GridAdvertiser;
            grids[2] = GridClient;
            grids[3] = GridHR;
            grids[4] = GridManager;
            grids[5] = GridCleaner;

            cmbRoles.ItemsSource = list;
            cmbRoles.SelectedIndex = 0;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string selectItem = cmbRoles.SelectedItem.ToString();
            string uri = $"Pages/{selectItem}/{(sender as Button).Tag.ToString()}.xaml";
            mainFrame.Source = new Uri(uri, UriKind.Relative);
        }

        private void cmbRoles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GridVisibility(cmbRoles.SelectedIndex);
        }

        public void GridVisibility(int index)
        {
            for(int i = 0; i < grids.Length; i++)
            {
                if(i == index)
                    grids[i].Visibility = Visibility.Visible;
                else
                    grids[i].Visibility = Visibility.Hidden;
            }
        }
    }
}
