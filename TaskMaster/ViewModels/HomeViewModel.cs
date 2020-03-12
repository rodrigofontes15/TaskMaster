using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TaskMaster.Models;

namespace TaskMaster.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Tasks> Tasks { get; set; }
        public string NomeTask { get; set; }
        [Display(Name = "Task Relacionada")]
        public int TasksId { get; set; }

        public IEnumerable<Projetos> Projetos { get; set; }
        [Display(Name = "Nome do Projeto")]
        public string NomeProjeto { get; set; }
        [Display(Name = "Nome do Projeto")]
        public int ProjetosId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data Estimada de Entrega")]
        public DateTime? DataEstimada { get; set; }
        [Display(Name = "Data Real de Entrega")]
        public DateTime? DataReal { get; set; }

        public IEnumerable<GerenteProjs> GerenteProjs { get; set; }
        [Display(Name = "Gerente de Projeto")]
        public int? GerenteProjsId { get; set; }

        public IEnumerable<Bugs> Bugs { get; set; }
        public int BugsId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data Estimada para Resolução")]
        public DateTime? DataEstimadaBug { get; set; }

        public IEnumerable<Devs> Devs { get; set; }
        public int DevsId { get; set; }
        public string DevNome { get; set; }
        public string EmailDev { get; set; }
        public string TelDev { get; set; }
        public string UrlPhotoDev { get; set; }

        public IEnumerable<TiposBugs> TiposBugs { get; set; }
        [Display(Name = "Tipo de Bug")]
        public string TipoBug { get; set; }
        [Display(Name = "Tipo de Bug")]
        public int TiposBugsId { get; set; }


    }
}