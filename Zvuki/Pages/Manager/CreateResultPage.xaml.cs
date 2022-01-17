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

namespace Zvuki.Pages.Manager
{
    /// <summary>
    /// Логика взаимодействия для CreateResultPage.xaml
    /// </summary>
    public partial class CreateResultPage : Page
    {

        ObservableCollection<Result> results = new ObservableCollection<Result>();
        ArrayList listCandidates = new ArrayList();

        public CreateResultPage()
        {
            InitializeComponent();
            cmpCandidate.ItemsSource = listCandidates;
            ResultList.ItemsSource = results;
            loadData();
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e) =>
            CreateResult();

        private void Button_Click_Update(object sender, RoutedEventArgs e) =>
          UpdateResult();

        private void Button_Click_Delete(object sender, RoutedEventArgs e) =>
            DeleteResult();

        public async void CreateResult()
        {
            await Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        Candidate c = cmpCandidate.SelectedItem as Candidate;

                        Result result = new Result
                        {
                            ResultTitle = txtResultTitle.Text,
                            Scores = Convert.ToInt32(txtScores.Text),
                            Candidate = db.Candidates
                            .FirstOrDefault(x => x.IdCandidate == c.IdCandidate)
                        };

                        db.Results.Add(result);
                        db.SaveChanges();
                        loadData();
                    });
                }
            });
        }

        public async void UpdateResult()
        {
            await Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {

                        Result re = results[ResultList.SelectedIndex];
                        Result result = db.Results
                        .FirstOrDefault(x => x.IdResult == re.IdResult);

                        Candidate c = cmpCandidate.SelectedItem as Candidate;

                        result.ResultTitle = txtResultTitle.Text;
                        result.Scores = Convert.ToInt32(txtScores.Text);
                        result.Candidate = db.Candidates
                        .FirstOrDefault(x => x.IdCandidate == c.IdCandidate);

                        
                        db.SaveChanges();
                        loadData();
                    });
                }
            });
        }

        public async void DeleteResult()
        {
            await Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        Result re = results[ResultList.SelectedIndex];
                        Result result = db.Results
                        .FirstOrDefault(x => x.IdResult == re.IdResult);

                        db.Results.Remove(result);
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
                        var candidates = db.Candidates.ToList();
                        var results = db.Results
                        .Include(x => x.Candidate)
                        .ThenInclude(x => x.Client)
                        .ThenInclude(x => x.Human)
                        .ToList();
                       
                        this.results.Clear();
                        this.listCandidates.Clear();

                        foreach (var vr in results)
                        {
                            this.results.Add(vr);
                        }
                        foreach (var vr in candidates)
                        {
                            this.listCandidates.Add(vr);
                        }
                    });
                }
            });
        }
    }
}
