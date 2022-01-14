using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        int amountMoney;
        Human human;
        ICollection<Group> groups;
        ICollection<Group> groupsInvitations;
        ICollection<Rent> rents;
        ICollection<AudioRecordingClient> audioRecordingClients;

        public virtual int AmountMoney
        {
            get { return amountMoney; }
            set
            {
                amountMoney = value;
                OnPropertyChanged("AmountMoney");
            }
        }

        public virtual Human Human
        {
            get { return human; }
            set
            {
                human = value;
                OnPropertyChanged("Human");
            }
        }

        public virtual ICollection<Group> Groups
        {
            get { return groups; }
            set
            {
                groups = value;
                OnPropertyChanged("Groups");
            }
        }
        public virtual ICollection<Group> GroupsInvitations
        {
            get { return groupsInvitations; }
            set
            {
                groupsInvitations = value;
                OnPropertyChanged("GroupsInvitations");
            }
        }

        public virtual ICollection<Rent> Rents
        {
            get { return rents; }
            set
            {
                rents = value;
                OnPropertyChanged("Rents");
            }
        }

        public virtual ICollection<AudioRecordingClient> AudioRecordingClients
        {
            get { return audioRecordingClients; }
            set
            {
                audioRecordingClients = value;
                OnPropertyChanged("AudioRecordingClients");
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
