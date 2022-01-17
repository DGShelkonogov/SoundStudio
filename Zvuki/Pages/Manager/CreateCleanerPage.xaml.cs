﻿using Microsoft.EntityFrameworkCore;
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

namespace Zvuki.Pages.Manager
{
    /// <summary>
    /// Логика взаимодействия для CreateCleanerPage.xaml
    /// </summary>
    public partial class CreateCleanerPage : Page
    {
        ObservableCollection<Cleaning> cleanings = new ObservableCollection<Cleaning>();
        ArrayList listEmployees = new ArrayList();
        ArrayList listRecordingRoom = new ArrayList();
        public CreateCleanerPage()
        {
            InitializeComponent();
            cmbEmployees.ItemsSource = listEmployees;
            cmbRecordingRoom.ItemsSource = listRecordingRoom;
            CleaningList.ItemsSource = cleanings;
            loadData();

        }

        private void Button_Click_Add(object sender, RoutedEventArgs e) => Create();

        private void Button_Click_Update(object sender, RoutedEventArgs e) => Update();

        private void Button_Click_Delete(object sender, RoutedEventArgs e) => Delete();



        public async void Create()
        {
            await Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        Employee e = cmbEmployees.SelectedItem as Employee;
                        RecordingRoom r = cmbRecordingRoom.SelectedItem as RecordingRoom;

                        Cleaning cleaning = new Cleaning
                        {
                            DateTime = dtpDate.DisplayDate,
                            Employee = db.Employees
                            .FirstOrDefault(x => x.IdEmployee == e.IdEmployee),
                            RecordingRoom = db.RecordingRooms
                          .FirstOrDefault(x => x.IdRecordingRoom == r.IdRecordingRoom)
                        };

                        // добавляем их в бд
                        db.Cleanings.Add(cleaning);
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
                        Cleaning c = cleanings[CleaningList.SelectedIndex];
                        Cleaning cleaning = db.Cleanings
                        .FirstOrDefault(x => x.IdCleaning == c.IdCleaning);


                        Employee e = cmbEmployees.SelectedItem as Employee;
                        RecordingRoom r = cmbRecordingRoom.SelectedItem as RecordingRoom;

                        cleaning.DateTime = dtpDate.DisplayDate;
                        cleaning.Employee = db.Employees
                            .FirstOrDefault(x => x.IdEmployee == e.IdEmployee);
                        cleaning.RecordingRoom = db.RecordingRooms
                      .FirstOrDefault(x => x.IdRecordingRoom == r.IdRecordingRoom);


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
                    var cleanings = db.Cleanings
                    .Include(x => x.Employee)
                    .ThenInclude(x => x.Human)
                    .Include(x => x.RecordingRoom)
                    .ToList();

                    var employess = db.Employees
                    .Include(x => x.Human)
                    .ToList();

                    var recordingRooms = db.RecordingRooms
                    .ToList();


                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        this.cleanings.Clear();
                        this.listEmployees.Clear();
                        this.listRecordingRoom.Clear();

                        foreach(var vr in cleanings)
                        {
                            this.cleanings.Add(vr);
                        }

                        foreach (var vr in employess)
                        {
                            listEmployees.Add(vr);
                        }
                        foreach(var vr in recordingRooms)
                        {
                            listRecordingRoom.Add(vr);
                        }
                    });
                }
            });

        }
    }
}