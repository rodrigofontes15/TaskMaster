﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Web;
using TaskMaster.Models;

namespace TaskMaster.ViewModels
{
    public class ProjetoViewModel
    {
        public IEnumerable<GerenteProjs> GerenteProjs { get; set; }
        
        public int? ProjetosId { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Nome do Projeto")]
        public string NomeProjeto { get; set; }

        [Display(Name = "Gerente de Projeto")]
        public int? GerenteProjsId { get; set; }

        [Required]
        [Display(Name = "Data de Início")]
        public DateTime? DataInicio { get; set; }

        [Required]
        [Display(Name = "Data Estimada de Término")]
        public DateTime? DataEstimada { get; set; }

        [Display(Name = "Estado do Projeto")]
        public string EstadoProj { get; set; }

        public DateTime? DataReal { get; set; }

        public string Titulo
        {
            get
            {
                return ProjetosId != 0 ? "Editar Projeto" : "Novo Projeto";
            }
        }

        public ProjetoViewModel()
        {
            ProjetosId = 0;
            DataInicio = DateTime.Today;
            EstadoProj = "Aberto";
        }

        public ProjetoViewModel(Projetos projeto)
        {
            ProjetosId = projeto.ProjetosId;
            NomeProjeto = projeto.NomeProjeto;
           // GerenteProjsId = projeto.GerenteProjsId;
            DataInicio = projeto.DataInicio;
            DataEstimada = projeto.DataEstimada;
            EstadoProj = projeto.EstadoProj;
        }

    }
}