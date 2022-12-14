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
    public class Cleaning
    {
        [Key]
        public int IdCleaning { get; set; }
        DateTime dateTime;
        Employee employee;
        RecordingRoom recordingRoom;


        [Required]
        [Column(TypeName = "Date")]
        public DateTime DateTime
        {
            get { return dateTime; }
            set
            {
                dateTime = value;
                OnPropertyChanged("DateTime");
            }
        }

        [Required]
        public virtual Employee Employee
        {
            get { return employee; }
            set
            {
                employee = value;
                OnPropertyChanged("Employee");
            }
        }

        [Required]
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
