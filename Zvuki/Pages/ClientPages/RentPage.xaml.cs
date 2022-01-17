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

namespace Zvuki.Pages.ClientPages
{
    /// <summary>
    /// Логика взаимодействия для RentPage.xaml
    /// </summary>
    public partial class RentPage : Page
    {
        ObservableCollection<Rent> rents = new ObservableCollection<Rent>();
        ArrayList equipments = new ArrayList();

        public RentPage()
        {
            InitializeComponent();
            loadData();
            cmbEquipment.ItemsSource = equipments;
            RentList.ItemsSource = rents;
        }

        private void Button_Click_ToRent(object sender, RoutedEventArgs e) => toRent();


        public async void toRent()
        {
            await Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        Client client = DataLoader.getClient();

                        Equipment eq = cmbEquipment.SelectedItem as Equipment;
                        Equipment equipment = db.Equipments
                           .FirstOrDefault(x => x.IdEquipment == eq.IdEquipment);
                        int amount = Convert.ToInt32(txtAmount.Text);

                        equipment.Amount -= amount;

                        Rent rent = new Rent
                        {
                            Amount = Convert.ToInt32(txtAmount.Text),
                            Price = eq.Price * amount,
                            StartDate = dpFrom.DisplayDate,
                            EndDate = dpTo.DisplayDate,
                            Equipment = db.Equipments
                            .FirstOrDefault(x => x.IdEquipment == eq.IdEquipment),
                            Client = db.Clients
                            .FirstOrDefault(x => x.IdClient == client.IdClient)
                        };

                        db.Rents.Add(rent);
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
                    Client client = DataLoader.getClient();
                    var equipments = db.Equipments.ToList();
                    var rents = db.Rents
                    .Include(x => x.Client)
                    .ThenInclude(x => x.Human)
                    .Where(x => x.Client.IdClient == client.IdClient)
                    .ToList();

                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        this.equipments.Clear();
                        this.rents.Clear();


                        foreach (var vr in equipments)
                        {
                            this.equipments.Add(vr);
                        }
                        foreach(var vr in rents)
                        {
                            this.rents.Add(vr);
                        }
                    });
                }
            });

        }

        private void Amount_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Equipment eq = cmbEquipment.SelectedItem as Equipment;
                int amount = Convert.ToInt32(txtAmount.Text);

                if(amount > eq.Amount)
                {
                    txtAmount.Text = eq.Amount.ToString();
                    labelPrice.Content = Convert.ToString("Price: " + eq.Price * eq.Amount + "$");
                }
                else
                {
                    labelPrice.Content = Convert.ToString("Price: " + eq.Price * amount + "$");
                }
            }
            catch(Exception ee)
            {
                labelPrice.Content = "Price: 0$";
            }
        }

        private void cmbEquipment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Equipment eq = cmbEquipment.SelectedItem as Equipment;
           labelAmount.Content = "Amount: " + eq.Amount;
           
        }
    }
}
