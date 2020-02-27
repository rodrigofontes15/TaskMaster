using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskMaster.Models
{
    public class TiposTestes
    {
        public int TiposTestesId { get; set; }

        public string TipoTeste { get; set; }

        public ICollection<Tasks> Tasks { get; set; }

    }
}