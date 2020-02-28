using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskMaster.Models
{
    public class EstadosBug
    {
        public int EstadosBugId { get; set; }

        public string NomeEstado { get; set; }

        public ICollection<Bugs> Bugs { get; set; }
    }
}