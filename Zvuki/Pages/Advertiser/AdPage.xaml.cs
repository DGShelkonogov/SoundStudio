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

namespace Zvuki.Pages.Advertiser
{
    public partial class AdPage : Page
    {

        ArrayList cmbListAdTypes = new ArrayList();
        ObservableCollection<AdvertisingOrder> advertisingOrders = new ObservableCollection<AdvertisingOrder>();
        public AdPage()
        {
            InitializeComponent();
            cmbTypeAd.ItemsSource = cmbListAdTypes;
            AdList.ItemsSource = advertisingOrders;
            loadData();
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            CreateAdType();
        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            UpdateAdType();
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            DaleteAdType();
        }


        public async void CreateAdType()
        {
            await Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        AdType ad = cmbTypeAd.SelectedItem as AdType;
                        Employee em = DataLoader.getEmployee();

                        AdvertisingOrder advertisingOrder = new AdvertisingOrder
                        {
                            AdType = db.AdTypes.FirstOrDefault(x => x.IdAdType == ad.IdAdType),
                            Price = Convert.ToInt32(txtPrice.Text),
                            OrderDate = DateTime.Now,
                           //ТУТ НАДА ИСПРАВИТЬ ПАТОМ
                            Employee = db.Employees.FirstOrDefault(x => x.IdEmployee == em.IdEmployee)
                        };

                        db.AdvertisingOrders.Add(advertisingOrder);
                        db.SaveChanges();
                        loadData();
                    });
                }
            });
        }

        public async void UpdateAdType()
        {
            await Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        AdvertisingOrder ao = advertisingOrders[AdList.SelectedIndex];
                        AdvertisingOrder advertisingOrder = db.AdvertisingOrders
                        .FirstOrDefault(x => x.IdAdvertisingOrder == ao.IdAdvertisingOrder);

                        advertisingOrder.AdType = cmbTypeAd.SelectedItem as AdType;
                        advertisingOrder.Price = Convert.ToInt32(txtPrice.Text);

                        db.SaveChanges();
                        loadData();
                    });
                }
            });
        }

        public async void DaleteAdType()
        {
            await Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        AdvertisingOrder ao = advertisingOrders[AdList.SelectedIndex];
                        AdvertisingOrder advertisingOrder = db.AdvertisingOrders
                        .FirstOrDefault(x => x.IdAdvertisingOrder == ao.IdAdvertisingOrder);
                        db.AdvertisingOrders.Remove(advertisingOrder);
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

                        var advertisingOrders = db.AdvertisingOrders
                        .Include(x => x.Employee)
                        .ThenInclude(x => x.Human)
                        .ToList();
                        var adTypes = db.AdTypes.ToList();

                        this.advertisingOrders.Clear();
                        this.cmbListAdTypes.Clear();

                        foreach (var advertisingOrder in advertisingOrders)
                        {
                            this.advertisingOrders.Add(advertisingOrder);
                        }
                        foreach(var adType in adTypes)
                        {
                            cmbListAdTypes.Add(adType);
                        }
                    });
                }
            });

        }
    }
}
