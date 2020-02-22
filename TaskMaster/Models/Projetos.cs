using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;    
using System.Linq;
using System.Web;

namespace TaskMaster.Models
{
    public class Projetos
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Nome do Projeto")]
        public string NomeProjeto { get; set; }

        [Display(Name = "Gerente de Projeto")]
        public int GerenteProjsId { get; set; }

        [ForeignKey("GerenteProjsId")]
        public GerenteProjs GerenteProjs { get; set; } //navigation class

        [Display(Name = "Data de Início")]
        public DateTime? DataInicio { get; set; }

        [Display(Name = "Data Estimada de Término")]
        public DateTime? DataEstimada { get; set; }

        public DateTime? DataReal { get; set; }
    }
}