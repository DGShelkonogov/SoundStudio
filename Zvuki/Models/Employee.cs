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

        [Required]
        [RegularExpression(@"\d*")]
        [StringLength(12, MinimumLength = 12)]
        public string INN
        {
            get { return inn; }
            set
            {
                inn = value;
                OnPropertyChanged("INN");
            }
        }

        [Required]
        [RegularExpression(@"\d*")]
        [StringLength(11, MinimumLength = 11)]
        public string SNILS
        {
            get { return snils; }
            set
            {
                snils = value;
                OnPropertyChanged("SNILS");
            }
        }

        [Required]
        public virtual Human Human
        {
            get { return human; }
            set
            {
                human = value;
                OnPropertyChanged("Human");
            }
        }
        [Required]
        public virtual BankAccount BankAccount
        {
            get { return bankAccount; }
            set
            {
                bankAccount = value;
                OnPropertyChanged("BankAccount");
            }
        }
        [Required]
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
