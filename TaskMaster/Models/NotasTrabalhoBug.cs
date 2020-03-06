using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskMaster.Models
{
    public class NotasTrabalhoBug
    {
       
            public int NotasTrabalhoBugId { get; set; }

            public string NotasTrabalho { get; set; }

            public DateTime DataNotaTrabalho { get; set; }

        public int BugsId { get; set; }
        [ForeignKey("BugsId")]
        public Bugs Bugs { get; set; }

    }
}