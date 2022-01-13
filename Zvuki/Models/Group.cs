using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zvuki.Models
{
    public class Group
    {
        [Key]
        public int IdGroup { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Rent> Rents { get; set; }

        public virtual ICollection<AudioRecordingGroup> 
            AudioRecordingGroups { get; set; }


    }
}
