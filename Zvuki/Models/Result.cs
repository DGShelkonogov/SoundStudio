using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zvuki.Models
{
    public class Result
    {
        [Key]
        public int IdResult { get; set; }
        Candidate candidate;
        int scores;
        string resultTitle;


        public virtual Candidate Candidate
        {
            get { return candidate; }
            set
            {
                candidate = value;
                OnPropertyChanged("Candidate");
            }
        }
        public int Scores
        {
            get { return scores; }
            set
            {
                scores = value;
                OnPropertyChanged("Scores");
            }
        }
        public string ResultTitle
        {
            get { return resultTitle; }
            set
            {
                resultTitle = value;
                OnPropertyChanged("ResultTitle");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
