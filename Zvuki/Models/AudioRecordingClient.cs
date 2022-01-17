using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zvuki.Models
{
    public class AudioRecordingClient
    {
        [Key]
        public int IdAudioRecordingClient { get; set; }
        AudioRecording audioRecording;
        Copyright copyright;

        public virtual AudioRecording AudioRecording
        {
            get { return audioRecording; }
            set
            {
                audioRecording = value;
                OnPropertyChanged("AudioRecording");
            }
        }
        
        public virtual Copyright Copyright
        {
            get { return copyright; }
            set
            {
                copyright = value;
                OnPropertyChanged("Copyright");
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
