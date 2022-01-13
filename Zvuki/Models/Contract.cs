using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zvuki.Models
{
    public class Contract
    {
        [Key]
        public int IdContract { get; set; }
        public string Path { get; set; }
        public virtual Employee Employee { get; set; }

    }
}
