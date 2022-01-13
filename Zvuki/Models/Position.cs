using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zvuki.Models
{
    public class Position
    {
        [Key]
        public int IdPosition { get; set; }
        public string Title { get; set; }
        public int Salary { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
