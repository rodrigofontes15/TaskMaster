using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TaskMaster.Models;

namespace TaskMaster.ViewModels
{
    public class BugsViewModel
    {
        public IEnumerable<Devs> Devs { get; set; }
        public string DevNome { get; set; }

        public IEnumerable<Tasks> Tasks { get; set; }
        public string Nome { get; set; }

        public int? TestersId { get; set; }

        public int? BugsId { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Descrição do Bug Encontrado")]
        public string DescBug { get; set; }

        [Display(Name = "Task Relacionada")]
        public int? TasksId { get; set; }

        [Display(Name = "Bug encontrado em:")]
        public DateTime? DataBug { get; set; }

        [Display(Name = "Data Estimada para Resolução")]
        public DateTime? DataEstimada { get; set; }

        [Display(Name = "Dev Responsável")]
        public int DevsId { get; set; }

        [Display(Name = "Assignar Bug para mim")]
        public Boolean BugAceito { get; set; }

        [Display(Name = "Repositório (URL GitHub)")]
        public string UrlRepoCodigo { get; set; }

        public string Titulo
        {
            get
            {
                return BugsId != 0 ? "Editar Bug" : "Reportar Bug";
            }
        }

        public BugsViewModel()
        {
            BugsId = 0;
        }

        public BugsViewModel(Bugs bug)
        {
            BugsId = bug.TasksId;
            DescBug = bug.DescBug;
            DataBug = bug.DataBug;
            DataEstimada = bug.DataEstimada;
            TasksId = bug.TasksId;
            DevsId = bug.DevsId;
            UrlRepoCodigo = bug.UrlRepoCodigo;

        }
    }
}