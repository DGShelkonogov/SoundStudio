using System;
using System.Collections.Generic;
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
        public string Login { get; set; }
        public string Email { get; set; }
        public string INN { get; set; }
        public string SNILS { get; set; }

        public virtual Human Human { get; set; }
        public virtual ICollection<Position> Positions { get; set; }


    }
}
