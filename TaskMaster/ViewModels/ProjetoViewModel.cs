using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using TaskMaster.Models;

namespace TaskMaster.ViewModels
{
    public class ProjetoViewModel
    {
        public IEnumerable<GerenteProjs> GerenteProjs { get; set; }
        
        public int? Id { get; set; }

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

        public DateTime? DataReal { get; set; }

        public string Titulo
        {
            get
            {
                return Id != 0 ? "Editar Projeto" : "Novo Projeto";
            }
        }

        public ProjetoViewModel()
        {
            Id = 0;
        }

        public ProjetoViewModel(Projetos projeto)
        {
            Id = projeto.Id;
            NomeProjeto = projeto.NomeProjeto;
            GerenteProjsId = projeto.GerenteProjsId;
            DataInicio = projeto.DataInicio;
            DataEstimada = projeto.DataEstimada;
        }

    }
}