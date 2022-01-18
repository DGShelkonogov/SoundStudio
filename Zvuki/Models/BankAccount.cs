using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zvuki.Models
{
    public class BankAccount
    {
        [Key]
        public int IdBankAccount { get; set; }
        string number, bank, bik, inn, kpp, korAccount;

        [Required]
        [RegularExpression(@"\d*")]
        [StringLength(20, MinimumLength = 20)]
        public string Number
        {
            get { return number; }
            set
            {
                number = value;
                OnPropertyChanged("Number");
            }
        }

        [Required]
        [RegularExpression(@"\D*")]
        [StringLength(50, MinimumLength = 5)]
        public string Bank
        {
            get { return bank; }
            set
            {
                bank = value;
                OnPropertyChanged("Bank");
            }
        }

        [Required]
        [RegularExpression(@"\d*")]
        [StringLength(9, MinimumLength = 9)]
        public string Bik
        {
            get { return bik; }
            set
            {
                bik = value;
                OnPropertyChanged("Bik");
            }
        }
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
        [StringLength(9, MinimumLength = 9)]
        public string KPP
        {
            get { return kpp; }
            set
            {
                kpp = value;
                OnPropertyChanged("KPP");
            }
        }
        [Required]
        [RegularExpression(@"\d*")]
        [StringLength(20, MinimumLength = 20)]
        public string KorAccount
        {
            get { return korAccount; }
            set
            {
                korAccount = value;
                OnPropertyChanged("KorAccount");
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
