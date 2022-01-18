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
    /// Логика взаимодействия для VoiceActingRolePage.xaml
    /// </summary>
    public partial class VoiceActingRolePage : Page
    {
        ObservableCollection<VoiceActingRole> voiceActingRoles = new ObservableCollection<VoiceActingRole>();

        public VoiceActingRolePage()
        {
            InitializeComponent();
            loadData();
            VoiceRoleList.ItemsSource = voiceActingRoles;
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            CreateVoiceRole();
        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            UpdateVoiceRole();
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            DaleteVoiceRole();
        }

        public async void CreateVoiceRole()
        {
            await Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    try
                    {
                        App.Current.Dispatcher.Invoke((Action)delegate
                        {
                            VoiceActingRole voiceActingRole = new VoiceActingRole()
                            {
                                Title = txtTitle.Text
                            };

                            if (MainWindow.validData(voiceActingRole))
                            {
                                db.VoiceActingRoles.Add(voiceActingRole);
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

        public async void UpdateVoiceRole()
        {
            await Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    try
                    {
                        App.Current.Dispatcher.Invoke((Action)delegate
                        {

                            VoiceActingRole vr = voiceActingRoles[VoiceRoleList.SelectedIndex];
                            VoiceActingRole voiceActingRole = db.VoiceActingRoles
                            .FirstOrDefault(x => x.IdVoiceActingRole == vr.IdVoiceActingRole);

                            voiceActingRole.Title = txtTitle.Text;

                            if (MainWindow.validData(voiceActingRole))
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

        public async void DaleteVoiceRole()
        {
            await Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    try
                    {

                        App.Current.Dispatcher.Invoke((Action)delegate
                        {

                            VoiceActingRole vr = voiceActingRoles[VoiceRoleList.SelectedIndex];
                            VoiceActingRole voiceActingRole = db.VoiceActingRoles
                            .FirstOrDefault(x => x.IdVoiceActingRole == vr.IdVoiceActingRole);

                            db.VoiceActingRoles.Remove(voiceActingRole);
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
                            var voiceActingRoles = db.VoiceActingRoles.ToList();
                            this.voiceActingRoles.Clear();
                            foreach (var vr in voiceActingRoles)
                            {
                                this.voiceActingRoles.Add(vr);
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

        private void VoiceRoleList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                VoiceActingRole vr = voiceActingRoles[VoiceRoleList.SelectedIndex];
                txtTitle.Text = vr.Title;

            }
            catch (Exception ex)
            {

            }
        }
    }
}

