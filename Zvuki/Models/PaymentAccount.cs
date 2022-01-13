using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zvuki.Models
{
    public class PaymentAccount
    {
        [Key]
        public int IdPaymentAccount { get; set; }
        public int SumPayment { get; set; }
        public DateTime DatePayment { get; set; }
        public virtual Employee Employee { get; set; }

    }
}
