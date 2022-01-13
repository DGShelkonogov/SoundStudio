using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zvuki.Models
{
    public class RecordingRoom
    {
        [Key]
        public int IdRecordingRoom { get; set; }
        public string RoomNumber { get; set; }
    }
}
