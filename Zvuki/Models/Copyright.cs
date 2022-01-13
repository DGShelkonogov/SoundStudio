using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zvuki.Models
{
    public class Copyright
    {
        [Key]
        public int IdCopyright { get; set; }
        public string Path { get; set; }
        public DateTime DateTime { get; set; }

        public virtual ICollection<AudioRecordingClient>
            AudioRecordingClients { get; set; }

        public virtual ICollection<AudioRecordingGroup>
            AudioRecordingGroups { get; set; }
    }
}
