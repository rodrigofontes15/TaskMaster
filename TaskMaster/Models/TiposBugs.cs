using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskMaster.Models
{
    public class TiposBugs
    {
        public int TiposBugsId { get; set; }

        public string TipoBug { get; set; }

        public ICollection<Bugs> Bugs { get; set; }

    }
}