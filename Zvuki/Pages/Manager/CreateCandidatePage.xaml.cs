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

namespace Zvuki.Pages.Manager;
    /// <summary>
    /// Логика взаимодействия для CreateCandidatePage.xaml
    /// </summary>
public partial class CreateCandidatePage : Page
{
    ObservableCollection<Candidate> candidates =
        new ObservableCollection<Candidate>();
    ObservableCollection<VoiceActingRole> voiceActingRoles = 
        new ObservableCollection<VoiceActingRole>();

    ArrayList listVoiceRoles = new ArrayList();
    ArrayList listClients = new ArrayList();

    public CreateCandidatePage()
    {
        InitializeComponent();
        CandidateList.ItemsSource = candidates;
        RolesList.ItemsSource = voiceActingRoles;
        cmbRoles.ItemsSource = listVoiceRoles;
        cmbClient.ItemsSource = listClients;

        loadData();
    }

    private void Button_Click_Add(object sender, RoutedEventArgs e)
    {
        CreateCandidate();
    }

    private void Button_Click_Update(object sender, RoutedEventArgs e)
    {
        UpdateCandidate();
    }

    private void Button_Click_Delete(object sender, RoutedEventArgs e)
    {
        DeleteEmployee();
    }



    public async void CreateCandidate()
    {
        await Task.Run(() =>
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                App.Current.Dispatcher.Invoke((Action)delegate
                {
                    Client client = cmbClient.SelectedItem as Client;

                    Candidate candidate = new Candidate()
                    {
                        Client = db.Clients
                        .FirstOrDefault(x => x.IdClient == client.IdClient),
                        VoiceActingRoles = new List<VoiceActingRole>()
                    };

                    foreach (var vr in voiceActingRoles)
                    {
                        VoiceActingRole voice = db.VoiceActingRoles
                        .FirstOrDefault(x => x.IdVoiceActingRole == vr.IdVoiceActingRole);

                        candidate.VoiceActingRoles.Add(voice);
                    }

                    // добавляем их в бд
                    db.Candidates.Add(candidate);
                    db.SaveChanges();
                    loadData();

                });
            }
        });
    }

    public async void UpdateCandidate()
    {
        await Task.Run(() =>
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                App.Current.Dispatcher.Invoke((Action)delegate
                {
                    Client client = cmbClient.SelectedItem as Client;

                    Candidate c = candidates[CandidateList.SelectedIndex];
                    Candidate candidate = db.Candidates
                    .Include(x => x.VoiceActingRoles)
                    .FirstOrDefault(x => x.IdCandidate == c.IdCandidate);

                    candidate.Client = db.Clients
                    .FirstOrDefault(x => x.IdClient == client.IdClient);
                    candidate.VoiceActingRoles.Clear();

                    foreach (var vr in voiceActingRoles)
                    {
                        VoiceActingRole voice = db.VoiceActingRoles
                        .FirstOrDefault(x => x.IdVoiceActingRole == vr.IdVoiceActingRole);

                        candidate.VoiceActingRoles.Add(voice);
                    }



                    db.SaveChanges();
                    loadData();
                });
            }
        });
    }


    public async void DeleteEmployee()
    {
        await Task.Run(() =>
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                App.Current.Dispatcher.Invoke((Action)delegate
                {
                    Candidate c = candidates[CandidateList.SelectedIndex];
                    Candidate candidate = db.Candidates
                    .Include(x => x.VoiceActingRoles)
                    .Include(x => x.Client)
                    .FirstOrDefault(x => x.IdCandidate == c.IdCandidate);

                    db.Candidates.Remove(candidate);
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
                var candidates = db.Candidates
                .Include(x => x.Client)
                .ThenInclude(x => x.Human)
                .ToList();

                var voiceActingRoles = db.VoiceActingRoles.ToList();

                var clients = db.Clients
                .Include(x => x.Human)
                .ToList();

                App.Current.Dispatcher.Invoke((Action)delegate
                {
                    this.candidates.Clear();
                    listVoiceRoles.Clear();

                    foreach (var vr in candidates)
                    {
                        this.candidates.Add(vr);
                    }

                    foreach (var vr in voiceActingRoles)
                    {
                        listVoiceRoles.Add(vr);
                    }


                    foreach (var vr in clients)
                    {
                        listClients.Add(vr);
                    }
                });
            }
        });
    }


    private void cmbRoles_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        VoiceActingRole role = cmbRoles.SelectedItem as VoiceActingRole;

        if (voiceActingRoles.Contains(role))
            voiceActingRoles.Remove(role);
        else
            voiceActingRoles.Add(role);
        
    }

    private void CandidateList_MouseDown(object sender, MouseButtonEventArgs e)
    {
        Candidate candidate = candidates[CandidateList.SelectedIndex];

        cmbClient.SelectedItem = candidate.Client;
        voiceActingRoles.Clear();
        foreach(var vr in candidate.VoiceActingRoles)
        {
            voiceActingRoles.Add(vr);
        }
    }
}
