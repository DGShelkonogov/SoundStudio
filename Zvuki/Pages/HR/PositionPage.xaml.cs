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

namespace Zvuki.Pages.HR
{
    /// <summary>
    /// Логика взаимодействия для PositionPage.xaml
    /// </summary>
    public partial class PositionPage : Page
    {
        ObservableCollection<Position> positions = new ObservableCollection<Position>();


        public PositionPage()
        {
            InitializeComponent();
            loadData();
            PositionList.ItemsSource = positions;
            
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            CreatePosition();
        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            UpdatePosition();
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            DaletePosition();  
        }

        public async void CreatePosition()
        {
            await Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {

                        Position position = new Position()
                        {
                            Title = txtTitle.Text,
                            Salary = Convert.ToInt32(txtSalary.Text)
                        };
                        // добавляем их в бд
                        db.Positions.Add(position);
                        db.SaveChanges();
                        positions.Add(position);

                    });
                }
            });
        }

        public async void UpdatePosition()
        {
            await Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                       
                        Position p = positions[PositionList.SelectedIndex];
                        Position position = db.Positions.FirstOrDefault(x => x.IdPosition == p.IdPosition);
                        position.Title = txtTitle.Text;
                        position.Salary = Convert.ToInt32(txtSalary.Text);
                        db.SaveChanges();
                        loadData();
                    });
                }
            });
        }

        public async void DaletePosition()
        {
            await Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        Position p = positions[PositionList.SelectedIndex];
                        Position position = db.Positions.FirstOrDefault(x => x.IdPosition == p.IdPosition);
                        db.Positions.Remove(position);
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
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        
                        var positions = db.Positions.ToList();
                        this.positions.Clear();
                        foreach (var position in positions)
                        {
                            this.positions.Add(position);
                        }
                    });
                }
            });

        }

        private void PositionList_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Position p = PositionList.SelectedItem as Position;
                txtSalary.Text = p.Salary.ToString();
                txtTitle.Text = p.Title.ToString();
            }
            catch (Exception ex)
            {

            }
      
        }
    }
}
