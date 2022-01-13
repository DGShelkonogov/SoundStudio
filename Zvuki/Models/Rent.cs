using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zvuki.Models
{
    public class Rent
    {
        [Key]
        public int IdRent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual Equipment Equipment { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Group> Group { get; set; }

    }
}
