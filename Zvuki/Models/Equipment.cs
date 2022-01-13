using System;
using System.Collections.Generic;
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
        public string Title { get; set; }
        public int Amount { get; set; }


    }
}
