using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskMaster.Models
{
    public class NotasTrabalhoBugs
    {
        public int? NotasTrabalhoBugsId { get; set; }

        public string NotasTrabalho { get; set; }

        public ICollection<Bugs> Bugs { get; set; }
    }
}