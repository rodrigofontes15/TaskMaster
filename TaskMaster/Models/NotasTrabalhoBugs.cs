using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskMaster.Models
{
    public class NotasTrabalhoBugs
    {
        public int NotasTrabalhoBugsId { get; set; }

        public string NotasTrabalho { get; set; }

        public DateTime DataNotaTrabalho { get; set; }

    }
}