using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zvuki.Models
{
    public class Candidate
    {
        [Key]
        public int IdCandidate { get; set; }
        ICollection<VoiceActingRole> voiceActingRoles;
        Client client;

        public virtual ICollection<VoiceActingRole> VoiceActingRoles
        {
            get { return voiceActingRoles; }
            set
            {
                voiceActingRoles = value;
                OnPropertyChanged("VoiceActingRoles");
            }
        }
        public virtual Client Client
        {
            get { return client; }
            set
            {
                client = value;
                OnPropertyChanged("Client");
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
