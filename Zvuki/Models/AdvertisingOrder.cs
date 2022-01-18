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
    public class AdvertisingOrder
    {
        [Key]
        public int IdAdvertisingOrder { get; set; }

        DateTime orderDate;

      
        int price;

        AdType adType;

        Employee employee;


        [Required]
        [Column(TypeName = "Date")]
        public DateTime OrderDate
        {
            get { return orderDate; }
            set
            {
                orderDate = value;
                OnPropertyChanged("OrderDate");
            }
        }

        [Required]
        [Range(1, 1000)]
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
        public virtual AdType AdType
        {
            get { return adType; }
            set
            {
                adType = value;
                OnPropertyChanged("AdType");
            }
        }
        [Required]
        public virtual Employee Employee
        {
            get { return employee; }
            set
            {
                employee = value;
                OnPropertyChanged("Employee");
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
