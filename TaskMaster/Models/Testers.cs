using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskMaster.Models
{
    public class Testers
    {

        public int TestersId { get; set; }

        [Required]
        [StringLength(255)]
        public string Nome { get; set; }

        public string EmailTester { get; set; }

        public ICollection<Tasks> Tasks { get; set; }
    }
}