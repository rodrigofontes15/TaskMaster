using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TaskMaster.Models;

namespace TaskMaster.ViewModels
{
    public class DataRealProjetosViewModel
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




    }
}