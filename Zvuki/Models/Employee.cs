using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zvuki.Models
{
    public class Employee
    {
        [Key]
        public int IdEmployee { get; set; }

        string inn, snils;
        Human human;
        BankAccount bankAccount;
        ICollection<Position> positions;
        public string INN
        {
            get { return inn; }
            set
            {
                inn = value;
                OnPropertyChanged("INN");
            }
        }
        public string SNILS
        {
            get { return snils; }
            set
            {
                snils = value;
                OnPropertyChanged("SNILS");
            }
        }
        public virtual Human Human
        {
            get { return human; }
            set
            {
                human = value;
                OnPropertyChanged("Human");
            }
        }
        public virtual BankAccount BankAccount
        {
            get { return bankAccount; }
            set
            {
                bankAccount = value;
                OnPropertyChanged("BankAccount");
            }
        }
        public virtual ICollection<Position> Positions
        {
            get { return positions; }
            set
            {
                positions = value;
                OnPropertyChanged("Positions");
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
