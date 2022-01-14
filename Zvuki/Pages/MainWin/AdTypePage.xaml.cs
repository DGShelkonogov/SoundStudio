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

namespace Zvuki
{
    /// <summary>
    /// Логика взаимодействия для AdTypePage.xaml
    /// </summary>
    public partial class AdTypePage : Page
    {
        ObservableCollection<AdType> listDropMails = new ObservableCollection<AdType>();

        public AdTypePage()
        {
            InitializeComponent();
            AddList.ItemsSource = listDropMails;
            ReadWriteAsync();
        }

        public async void ReadWriteAsync()
        {
            await Task.Run(() => {
                using (ApplicationContext db = new ApplicationContext())
                {
                    // получаем объекты из бд и выводим на консоль
                    var ads = db.AdTypes.ToList();

                    foreach (AdType adType in ads)
                    {
                        App.Current.Dispatcher.Invoke((Action)delegate
                        {
                            listDropMails.Add(adType);
                        });
                    }

                }
            });
        }

        private void Button_Click_Add_Ad(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                // создаем два объекта User
                String str = AdType.Text;
                AdType ad = new AdType { Title = str };

                // добавляем их в бд
                db.AdTypes.Add(ad);
                listDropMails.Add(ad);
                db.SaveChanges();
            }
        }


    }
}
