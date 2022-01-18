using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zvuki.Models
{
    public class Equipment
    {
        [Key]
        public int IdEquipment { get; set; }

        string title;
        int amount, price;

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        [Required]
        [Range(1, 1000)]
        [StringLength(50, MinimumLength = 3)]
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
        [Range(1, 1000)]
        [StringLength(50, MinimumLength = 3)]
        public int Price 
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged("Price");
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
