
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Zvuki.Models
{
    public class Group
    {
        [Key]
        public int IdGroup { get; set; }

        string title;
        ICollection<Client> clients;
        ICollection<Rent> rents;
        ICollection<AudioRecordingGroup> audioRecordingGroups;

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }
        [Required]
        public virtual ICollection<Client> Clients
        {
            get { return clients; }
            set
            {
                clients = value;
                OnPropertyChanged("Clients");
            }
        }
        [Required]
        public virtual ICollection<Rent> Rents 
        {
            get { return rents; }
            set
            {
                rents = value;
                OnPropertyChanged("Rents");
            }
        }
        [Required]
        public virtual ICollection<AudioRecordingGroup> AudioRecordingGroups 
        {
            get { return audioRecordingGroups; }
            set
            {
                audioRecordingGroups = value;
                OnPropertyChanged("AudioRecordingGroups");
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
