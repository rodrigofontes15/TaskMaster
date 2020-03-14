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
        [Display(Name = "Dev Responsável")]
        public int DevsId { get; set; }

        public IEnumerable<Tasks> Tasks { get; set; }
        public string NomeTask { get; set; }
        [Display(Name = "Task Relacionada")]
        public int TasksId { get; set; }

        public IEnumerable<Projetos> Projetos { get; set; }
        [Display(Name = "Nome do Projeto")]
        public string NomeProjeto { get; set; }
        [Display(Name = "Nome do Projeto")]
        public int ProjetosId { get; set; }

        public IEnumerable<TiposBugs> TiposBugs { get; set; }
        [Display(Name = "Tipo de Bug")]
        public string TipoBug { get; set; }
        [Display(Name = "Tipo de Bug")]
        public int TiposBugsId { get; set; }

        public IEnumerable<EstadosBug> EstadosBugs { get; set; }
        [Display(Name = "Estado da Correção do Bug")]
        public int EstadosBugId { get; set; }
        [Display(Name = "Estado da Correção do Bug")]
        public int NomeEstado { get; set; }

        public int? TestersId { get; set; }

        public int? BugsId { get; set; }

        public IEnumerable<Bugs> Bugs { get; set; }
        [Required]
        [StringLength(255)]
        [Display(Name = "Descrição do Bug Encontrado")]
        public string DescBug { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data do Bug")]
        public DateTime? DataBug { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data Estimada para Resolução")]
        public DateTime? DataEstimadaBug { get; set; }
        [Display(Name = "Assignar Bug para mim")]
        public Boolean BugAceito { get; set; }
        [Display(Name = "Repositório (URL GitHub)")]
        public string UrlRepoCodigo { get; set; }
        public Double? TempoSolucao { get; set; }

        public IEnumerable<NotasTrabalhoBug> NotasTrabalhoBugs { get; set; }
        public int? NotasTrabalhoBugsId { get; set; }
        public string NotasTrabalho { get; set; }
        public DateTime DataNotaTrabalho { get; set; }

        public string ErrorMessage { get; set; }

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
            EstadosBugId = 2;
            
        }

        public BugsViewModel(Bugs bug)
        {
            BugsId = bug.BugsId;
            DescBug = bug.DescBug;
            DataBug = bug.DataBug;
            DataEstimadaBug = bug.DataEstimadaBug;
          //  ProjetosId = bug.ProjetosId;
            TasksId = bug.TasksId;
            DevsId = bug.DevsId;
            TiposBugsId = bug.TiposBugsId;
            EstadosBugId = bug.EstadosBugId;
            UrlRepoCodigo = bug.UrlRepoCodigo;

        }

        public BugsViewModel(NotasTrabalhoBug notasTrabalhoBugs)
        {
            BugsId = notasTrabalhoBugs.BugsId;
            NotasTrabalhoBugsId = notasTrabalhoBugs.NotasTrabalhoBugId;
            NotasTrabalho = notasTrabalhoBugs.NotasTrabalho;
            DataNotaTrabalho=notasTrabalhoBugs.DataNotaTrabalho;
        }

    }
}