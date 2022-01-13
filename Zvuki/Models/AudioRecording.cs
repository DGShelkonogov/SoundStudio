using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zvuki.Models
{
    public class AudioRecording
    {
        [Key]
        public int IdAudioRecording { get; set; }
        string path;
        DateTime dateOfCreate;
        RecordingRoom recordingRoom;


        public string Path
        {
            get { return path; }
            set
            {
                path = value;
                OnPropertyChanged("Path");
            }
        }
        public DateTime DateOfCreate
        {
            get { return dateOfCreate; }
            set
            {
                dateOfCreate = value;
                OnPropertyChanged("DateOfCreate");
            }
        }
        public virtual RecordingRoom RecordingRoom
        {
            get { return recordingRoom; }
            set
            {
                recordingRoom = value;
                OnPropertyChanged("RecordingRoom");
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
