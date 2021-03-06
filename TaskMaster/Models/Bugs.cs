﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TaskMaster.Models.Validacoes;

namespace TaskMaster.Models
{
    public class Bugs
    {
        public int BugsId { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Descrição do Bug Encontrado")]
        public string DescBug { get; set; }

        [Display(Name = "Task Relacionada")]
        public int TasksId { get; set; }
        [ForeignKey("TasksId")]
        public Tasks Tasks { get; set; } //navigation class

        //  [Display(Name = "Projeto Relacionado")]
        //   public int ProjetosId { get; set; }
        //[ForeignKey("ProjetosId")]
        //public Projetos Projetos { get; set; } //navigation class

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Bug en contrado em:")]
        public DateTime? DataBug { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data Estimada para Resolução")]
      //  [ValidarDataEstimadaBud(ErrorMessage =
        //    "Data estimada não pode ser menor que data de abertura do bug")]
        public DateTime? DataEstimadaBug { get; set; }

        public Double? TempoSolucao { get; set; }

        public int DevsId { get; set; }
        [ForeignKey("DevsId")]
        public Devs Devs { get; set; } //navigation class

        public Boolean BugAceito { get; set; }

        public string UrlRepoCodigo { get; set; }

        public int TiposBugsId { get; set; }
        [ForeignKey("TiposBugsId")]
        public TiposBugs TiposBugs { get; set; } //navigation class

        public int EstadosBugId { get; set; }
        [ForeignKey("EstadosBugId")]
        public EstadosBug EstadosBug { get; set; }

    }
}