using Microsoft.EntityFrameworkCore;
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
  
    public partial class ResultPage : Page
    {
        ObservableCollection<Result> results = new ObservableCollection<Result>();
        ObservableCollection<VoiceActingRole> roles =
            new ObservableCollection<VoiceActingRole>();
        public ResultPage()
        {
            InitializeComponent();
            ResultList.ItemsSource = results;
            RolesList.ItemsSource = roles;
            loadData();
        }


        public async void loadData()
        {
            await Task.Run(() =>
            {

                using (ApplicationContext db = new ApplicationContext())
                {
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {

                        Client client = DataLoader.getClient();

                        var candidates = db.Candidates.ToList();

                        var results = db.Results
                        .Include(x => x.Candidate)
                        .ThenInclude(x => x.VoiceActingRoles)
                        .Include(x => x.Candidate)
                        .ThenInclude(x => x.Client)
                        .ThenInclude(x => x.Human)
                        .Where(x => x.Candidate.Client.IdClient == client.IdClient)
                        .ToList();

                        this.results.Clear();

                        foreach (var vr in results)
                        {
                            this.results.Add(vr);
                        }
                       
                    });
                }
            });

        }

        private void ResultList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Result result = results[ResultList.SelectedIndex];
            roles.Clear();
            foreach (VoiceActingRole role in result.Candidate.VoiceActingRoles)
            {
                roles.Add(role);
            }
        }
    }
}
