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
    public class PaymentAccount
    {
        [Key]
        public int IdPaymentAccount { get; set; }
        int sumPayment;
        DateTime datePayment;
        Employee employee;

        public int SumPayment
        {
            get { return sumPayment; }
            set
            {
                sumPayment = value;
                OnPropertyChanged("SumPayment");
            }
        }
        [Column(TypeName = "Date")]
        public DateTime DatePayment
        {
            get { return datePayment; }
            set
            {
                datePayment = value;
                OnPropertyChanged("DatePayment");
            }
        }
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
