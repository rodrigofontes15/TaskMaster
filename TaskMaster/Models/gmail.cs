using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskMaster.Models
{
    public class gmail
    {
        [Display(Name = "Enviar Para")]
        public string To { get; set; }

        [Display(Name = "E-mail do Solicitante")]
        public string From { get; set; }

        [Required]
        [Display(Name = "Tipo de Role")]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "Detalhe sua Requisição")]
        public string Body { get; set; }
    }
}