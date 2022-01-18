using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Zvuki.Models;
using Annotation = System.ComponentModel.DataAnnotations;

namespace Zvuki
{
    public partial class MainWindow : Window
    {
       
        
        Dictionary<String, Grid> dictionaryAllPositions = new Dictionary<string, Grid>();
        Dictionary<String, Grid> dictionaryPositions = new Dictionary<string, Grid>();

        public MainWindow()
        {
            InitializeComponent();

            Human human = DataLoader.getHuman();

            dictionaryAllPositions.Add("ClientPages", GridClient);
            dictionaryAllPositions.Add("Advertiser", GridAdvertiser);
            dictionaryAllPositions.Add("Accountant", GridAccountant);
            dictionaryAllPositions.Add("HR", GridHR);
            dictionaryAllPositions.Add("Manager", GridManager);
            dictionaryAllPositions.Add("Сleaner", GridCleaner);

            using (ApplicationContext db = new ApplicationContext())
            {
                Employee employee = db.Employees
                    .Include(x => x.Human)
                    .Include(x => x.Positions)
                    .FirstOrDefault(x => x.Human.IdHuman == human.IdHuman);

                var employess = db.Employees
                    .Include(x => x.Human)
                    .ToList();

                Client client = db.Clients
                    .Include(x => x.Human)
                     .FirstOrDefault(x => x.Human.Login.Equals(human.Login));

                if(client != null)
                {
                    dictionaryPositions.Add("ClientPages", dictionaryAllPositions["ClientPages"]);
                }
                if(employee != null)
                {
                    foreach(Position p in employee.Positions)
                    {
                        if(dictionaryAllPositions.ContainsKey(p.Title))
                            dictionaryPositions.Add(p.Title, dictionaryAllPositions[p.Title]);
                    }
                }
            }

            cmbRoles.ItemsSource = dictionaryPositions.Keys;         
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
            GridVisibility(cmbRoles.SelectedItem as String);
        }

        public void GridVisibility(string key)
        {
           foreach(var item in dictionaryPositions)
           {
               item.Value.Visibility = Visibility.Hidden;
           }

           dictionaryPositions[key].Visibility = Visibility.Visible;
        }

        private void Button_Click_LogOut(object sender, RoutedEventArgs e)
        {
            DataLoader.DeleteFile(DataLoader.HUMAN_FILE);
            DataLoader.DeleteFile(DataLoader.CLIENT_FILE);
            DataLoader.DeleteFile(DataLoader.EMPLOYEE_FILE);
            StartWindow window = new StartWindow();
            window.Show();
            this.Close();
        }

        public static bool validData(Object args)
        {
            var results = new List<Annotation.ValidationResult>();
            var context = new ValidationContext(args);
            if (!Validator.TryValidateObject(args, context, results, true))
            {
                string message = "";
                foreach (var error in results)
                {
                    message += error.ErrorMessage + '\n';
                }
                MessageBox.Show(message);
                return false;
            }
            return true;
        }
    }
}
