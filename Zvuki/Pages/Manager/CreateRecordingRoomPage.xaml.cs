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
    /// Логика взаимодействия для CreateRecordingRoomPage.xaml
    /// </summary>
    public partial class CreateRecordingRoomPage : Page
    {
        ObservableCollection<RecordingRoom> recordingRooms = new ObservableCollection<RecordingRoom>();

        public CreateRecordingRoomPage()
        {
            InitializeComponent();
            RecordingRoomList.ItemsSource = recordingRooms;
            loadData();
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e) =>
            Create();

        private void Button_Click_Update(object sender, RoutedEventArgs e) =>
            Update();

        private void Button_Click_Delete(object sender, RoutedEventArgs e) =>
            Delete();


        public async void Create()
        {
            await Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    try
                    {

                        App.Current.Dispatcher.Invoke((Action)delegate
                        {
                            RecordingRoom recording = new RecordingRoom
                            {
                                RoomNumber = txtNumber.Text
                            };

                            if (MainWindow.validData(recording))
                            {
                                db.RecordingRooms.Add(recording);
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

        public async void Update()
        {
            await Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    try
                    {
                        App.Current.Dispatcher.Invoke((Action)delegate
                        {
                            RecordingRoom rc = recordingRooms[RecordingRoomList.SelectedIndex];
                            RecordingRoom recording = db.RecordingRooms
                            .FirstOrDefault(x => x.IdRecordingRoom == rc.IdRecordingRoom);

                            recording.RoomNumber = txtNumber.Text;

                            if (MainWindow.validData(recording))
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

        public async void Delete()
        {
            await Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    try
                    {
                        App.Current.Dispatcher.Invoke((Action)delegate
                        {
                            RecordingRoom rc = recordingRooms[RecordingRoomList.SelectedIndex];
                            RecordingRoom recording = db.RecordingRooms
                            .FirstOrDefault(x => x.IdRecordingRoom == rc.IdRecordingRoom);

                            db.RecordingRooms.Remove(recording);
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

                            var recordingRooms = db.RecordingRooms.ToList();
                            this.recordingRooms.Clear();
                            foreach (var adT in recordingRooms)
                            {
                                this.recordingRooms.Add(adT);
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

        private void RecordingRoomList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                RecordingRoom rc = recordingRooms[RecordingRoomList.SelectedIndex];
                txtNumber.Text = rc.RoomNumber;

            }
            catch (Exception ex)
            {

            }
        }
    }
}
