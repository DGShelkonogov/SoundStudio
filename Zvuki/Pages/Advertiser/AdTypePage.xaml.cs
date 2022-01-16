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

namespace Zvuki.Pages.Advertiser
{
    public partial class AdTypePage : Page
    {
        ObservableCollection<AdType> adTypes = new ObservableCollection<AdType>();
        public AdTypePage()
        {
            InitializeComponent();
            loadData();
            AdTypeList.ItemsSource = adTypes;
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

                        AdType adType = new AdType
                        {
                            Title = txtTitle.Text
                        };

                        db.AdTypes.Add(adType);
                        db.SaveChanges();
                        adTypes.Add(adType);
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
                        AdType at = adTypes[AdTypeList.SelectedIndex];
                        AdType adType = db.AdTypes.FirstOrDefault(x => x.IdAdType == at.IdAdType);

                        adType.Title = txtTitle.Text;
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
                        AdType at = adTypes[AdTypeList.SelectedIndex];
                        AdType adType = db.AdTypes.FirstOrDefault(x => x.IdAdType == at.IdAdType);
                        db.AdTypes.Remove(adType);
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

                        var adTypes = db.AdTypes.ToList();
                        this.adTypes.Clear();
                        foreach (var adT in adTypes)
                        {
                            this.adTypes.Add(adT);
                        }
                    });
                }
            });

        }

    }
}
