using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zvuki.Models
{
    public class Result
    {
        [Key]
        public int IdResult { get; set; }
        public virtual Candidate Candidate { get; set; }
        public int Scores { get; set; }
        public string ResultTitle { get; set; }
      
    }
}
