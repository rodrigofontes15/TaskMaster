using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaskMaster.Models
{
    public class Tasks
    {
        public int TasksId { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Tarefa de Teste")]
        public string NomeTask { get; set; }

        [Display(Name = "Projeto Relacionado")]
        public int ProjetosId { get; set; }

        [ForeignKey("ProjetosId")]
        public Projetos Projetos { get; set; } //navigation class

        [Display(Name = "Data de Início do Teste")]
        public DateTime? DataInicio { get; set; }

        [Display(Name = "Data Estimada de Término do Teste")]
        public DateTime? DataEstimada { get; set; }

        public int TestersId { get; set; }
        [ForeignKey("TestersId")]
        public Testers Testers { get; set; } //navigation class

        public DateTime? DataReal { get; set; }
    }
}