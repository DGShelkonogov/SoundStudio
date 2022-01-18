using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zvuki.Models
{
    public class Rent
    {
        [Key]
        public int IdRent { get; set; }

        DateTime startDate, endDate;
        int amount, price;
        Equipment equipment;
        Client client;

        [Required]
        [Column(TypeName = "Date")]
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                OnPropertyChanged("StartDate");
            }
        }
        [Required]
        [Column(TypeName = "Date")]
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
                OnPropertyChanged("EndDate");
            }
        }
        [Required]
        [Range(1, 999999)]
        public int Amount 
        {
            get { return amount; }
            set
            {
                amount = value;
                OnPropertyChanged("Amount");
            }
        }
        [Required]
        [Range(1, 999999)]
        public int Price 
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged("Price");
            }
        }
        [Required]
        public virtual Equipment Equipment
        {
            get { return equipment; }
            set
            {
                equipment = value;
                OnPropertyChanged("Equipment");
            }
        }
        [Required]
        public virtual Client Client
        {
            get { return client; }
            set
            {
                client = value;
                OnPropertyChanged("Client");
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
