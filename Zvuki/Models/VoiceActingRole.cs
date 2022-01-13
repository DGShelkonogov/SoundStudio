using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zvuki.Models
{
    public class VoiceActingRole
    {
        [Key]
        public int IdVoiceActingRole { get; set; }
        public string Title { get; set; }
    }
}
