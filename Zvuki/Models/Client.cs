using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zvuki.Models
{
    public class Client
    {
        [Key]
        public int IdClient { get; set; }
        public string AmountMoney { get; set; }
        public virtual Human Human { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Rent> Rents { get; set; }

        public virtual ICollection<AudioRecordingClient>
            AudioRecordingClients { get; set; }


    }
}
