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
    public class AudioRecording
    {
        [Key]
        public int IdAudioRecording { get; set; }
        string path;
        DateTime dateOfCreate;


        [Required]
        public string Path
        {
            get { return path; }
            set
            {
                path = value;
                OnPropertyChanged("Path");
            }
        }

        [Required]
        [Column(TypeName = "Date")]
        public DateTime DateOfCreate
        {
            get { return dateOfCreate; }
            set
            {
                dateOfCreate = value;
                OnPropertyChanged("DateOfCreate");
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
