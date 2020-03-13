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
        [Display(Name = "Nome do Projeto")]
        public int ProjetosId { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Nome do Projeto")]
        public string NomeProjeto { get; set; }

        [Display(Name = "Gerente de Projeto")]
        public int GerenteProjsId { get; set; }
        [ForeignKey("GerenteProjsId")]
        public GerenteProjs GerenteProjs { get; set; } //navigation class

        [Required]
        [Display(Name = "Data de Início")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DataInicio { get; set; }

        [Required]
        [Display(Name = "Data Estimada de Término")]
        public DateTime? DataEstimada { get; set; }

        public int QtdBugsPrj { get; set; }

        public DateTime? DataReal { get; set; }

     //   public ICollection<Bugs> Bugs { get; set; }
    }
}