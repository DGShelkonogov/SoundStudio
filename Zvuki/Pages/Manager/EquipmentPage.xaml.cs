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

namespace Zvuki.Pages.Manager
{
    /// <summary>
    /// Логика взаимодействия для EquipmentPage.xaml
    /// </summary>
    public partial class EquipmentPage : Page
    {

        ObservableCollection<Equipment> equipments = new ObservableCollection<Equipment>();
        public EquipmentPage()
        {
            InitializeComponent();
            loadData();
            EquipmentsList.ItemsSource = equipments;
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
            => CreateEquipment();
        

        private void Button_Click_Update(object sender, RoutedEventArgs e)
            => UpdateEquipment();
        


        private void Button_Click_Delete(object sender, RoutedEventArgs e) 
            => DaleteEquipment();
        
        public async void CreateEquipment()
        {
            await Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    try
                    {

                        App.Current.Dispatcher.Invoke((Action)delegate
                        {


                            Equipment equipment = new Equipment()
                            {
                                Amount = Convert.ToInt32(txtAmount.Text),
                                Price = Convert.ToInt32(txtPrice.Text),
                                Title = txtTitle.Text
                            };


                            if (MainWindow.validData(equipment))
                            {
                                db.Equipments.Add(equipment);
                                db.SaveChanges();
                                loadData();
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

        public async void UpdateEquipment()
        {
            await Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    try
                    {

                        App.Current.Dispatcher.Invoke((Action)delegate
                        {
                            Equipment eq = equipments[EquipmentsList.SelectedIndex];
                            Equipment equipment = db.Equipments
                            .FirstOrDefault(x => x.IdEquipment == eq.IdEquipment);


                            equipment.Amount = Convert.ToInt32(txtAmount.Text);
                            equipment.Price = Convert.ToInt32(txtPrice.Text);
                            equipment.Title = txtTitle.Text;

                            if (MainWindow.validData(equipment))
                            {
                                db.SaveChanges();
                                loadData();
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

        public async void DaleteEquipment()
        {
            await Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    try
                    {
                        App.Current.Dispatcher.Invoke((Action)delegate
                        {
                            Equipment eq = equipments[EquipmentsList.SelectedIndex];
                            Equipment equipment = db.Equipments
                            .FirstOrDefault(x => x.IdEquipment == eq.IdEquipment);

                            db.Equipments.Remove(equipment);
                            db.SaveChanges();
                            loadData();
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                   
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
                        App.Current.Dispatcher.Invoke((Action)delegate
                        {

                            var equipments = db.Equipments.ToList();
                            this.equipments.Clear();
                            foreach (var vr in equipments)
                            {
                                this.equipments.Add(vr);
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

        private void EquipmentsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Equipment eq = equipments[EquipmentsList.SelectedIndex];
                txtAmount.Text = eq.Amount.ToString();
                txtPrice.Text = eq.Price.ToString();
                txtTitle.Text = eq.Title;
            }
            catch (Exception ex)
            {

            }

        }
    }
}

