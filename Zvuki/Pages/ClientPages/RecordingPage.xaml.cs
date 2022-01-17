using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
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

namespace Zvuki.Pages.ClientPages
{
    public partial class RecordingPage : Page
    {

        string pathRecording, pathCopyright;
        ObservableCollection<AudioRecordingClient> audioRecordingClients 
            = new ObservableCollection<AudioRecordingClient>();
        public RecordingPage()
        {
            InitializeComponent();
            RecordingList.ItemsSource = audioRecordingClients;
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e) => Create();

        private void Button_Click_Delete(object sender, RoutedEventArgs e) => Update();

        private void Button_Click_Update(object sender, RoutedEventArgs e) => Delete();

        private void Button_Click_Upload_Recording(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Docx files (*.docx)|*.docx|Text files (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == true)
            {
                pathRecording = openFileDialog.FileName;
                txtTitle.Content = "File: " + pathRecording;
            }
        }

        private void Button_Click_Upload_Copyright(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Docx files (*.docx)|*.docx|Text files (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == true)
            {
                pathCopyright = openFileDialog.FileName;
                txtCopyright.Content = "File: " + pathCopyright;
            }
        }

        public async void loadData()
        {
            await Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Client cl = DataLoader.getClient();
                    var client = db.Clients
                    .Include(x => x.AudioRecordingClients)
                    .FirstOrDefault(x => x.IdClient == cl.IdClient);

                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        this.audioRecordingClients.Clear();
                        foreach (var vr in client.AudioRecordingClients)
                        {
                            this.audioRecordingClients.Add(vr);
                        }
                    });
                }
            });
        }

        public async void Create()
        {
            await Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        Client c = DataLoader.getClient();
                        Client client = db.Clients
                           .FirstOrDefault(x => x.IdClient == c.IdClient);
                        if(client.AudioRecordingClients == null)
                        {
                            client.AudioRecordingClients = new List<AudioRecordingClient>();
                        }


                        AudioRecordingClient audioRecordingClient = new AudioRecordingClient
                        {
                            AudioRecording = new AudioRecording
                            {
                                DateOfCreate = DateTime.Now,
                                Path = pathRecording
                            },
                            Copyright = new Copyright
                            {
                                DateTime = DateTime.Now,
                                Path = pathCopyright
                            }
                        };
                        client.AudioRecordingClients.Add(audioRecordingClient);
                        db.SaveChanges();
                        loadData();
                    });
                }
            });
        }

        public async void Update()
        {
            await Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        AudioRecordingClient ar =
                            audioRecordingClients[RecordingList.SelectedIndex];

                        AudioRecordingClient audioRecordingClient = db.AudioRecordingClients.
                        FirstOrDefault(x => x.IdAudioRecordingClient == ar.IdAudioRecordingClient);

                        audioRecordingClient.AudioRecording.Path = pathRecording;
                        audioRecordingClient.Copyright.Path = pathCopyright;

                        db.SaveChanges();
                        loadData();
                    });
                }
            });
        }

        public async void Delete()
        {
            await Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        AudioRecordingClient ar =
                             audioRecordingClients[RecordingList.SelectedIndex];

                        AudioRecordingClient audioRecordingClient = db.AudioRecordingClients.
                        FirstOrDefault(x => x.IdAudioRecordingClient == ar.IdAudioRecordingClient);
                        db.AudioRecordingClients.Remove(audioRecordingClient);
                        db.SaveChanges();
                        loadData();
                    });
                }
            });
        }

    }
}
