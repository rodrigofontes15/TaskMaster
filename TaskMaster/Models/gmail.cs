using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskMaster.Models
{
    public class gmail
    {
        public string To { get; set; } = "rodrigofontes1985@gmail.com";

        public string From { get; set; }

        [Required]
        [Display(Name = "Tipo de Role")]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "Detalhe sua Requisição")]
        public string Body { get; set; }
    }
}