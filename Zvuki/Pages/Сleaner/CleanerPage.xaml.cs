using Microsoft.EntityFrameworkCore;
using System;
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

namespace Zvuki.Pages.Сleaner
{
    /// <summary>
    /// Логика взаимодействия для CleanerPage.xaml
    /// </summary>
    public partial class CleanerPage : Page
    {

        ObservableCollection<Cleaning> cleanings = new ObservableCollection<Cleaning>();


        public CleanerPage()
        {
            InitializeComponent();
            CleaningList.ItemsSource = cleanings;
            loadData();
        }

        private void Button_Click_Done(object sender, RoutedEventArgs e) => Delete();

        public async void Delete()
        {
            await Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        Cleaning c = cleanings[CleaningList.SelectedIndex];
                        Cleaning cleaning = db.Cleanings
                        .FirstOrDefault(x => x.IdCleaning == c.IdCleaning);

                        db.Cleanings.Remove(cleaning);
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
                    Employee employee = DataLoader.getEmployee();

                    var cleanings = db.Cleanings
                    .Include(x => x.Employee)
                    .ThenInclude(x => x.Human)
                    .Include(x => x.RecordingRoom)
                    .Where(x => x.Employee.IdEmployee == employee.IdEmployee)
                    .ToList();

                  
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        this.cleanings.Clear();


                        foreach (var vr in cleanings)
                        {
                            this.cleanings.Add(vr);
                        }
                    });
                }
            });

        }
    }
}
