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

        public DateTime? DataReal { get; set; } //propriedade usada para trasportar o maior DataEstimadaBug dentre todos os bugs da task até a DataReal do Projetos

        public int TiposTestesId { get; set; }
        [ForeignKey("TiposTestesId")]
        public TiposTestes TiposTestes { get; set; } //navigation class

        public int QtdBugsTsk { get; set; }

        [Display(Name = "Estado da Task")]
        public string EstadoTask { get; set; }

        public string Titulo
        {
            get
            {
                return TasksId != 0 ? "Detalhes da Task" + NomeTask : "Nova Task";
            }
        }
    }
}