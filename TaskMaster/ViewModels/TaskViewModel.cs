using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TaskMaster.Models;

namespace TaskMaster.ViewModels
{
    public class TaskViewModel
    {
        public IEnumerable<Projetos> Projetos { get; set; }
        public string NomeProjeto { get; set; }

        public IEnumerable<Testers> Testers { get; set; }
        public string Nome { get; set; }

        public int TasksId {get; set;}
        public int? Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Tarefa de Teste")]
        public string NomeTask { get; set; }

        [Display(Name = "Projeto Relacionado")]
        public int ProjetosId { get; set; }

        [Display(Name = "Data de Início do Teste")]
        public DateTime? DataInicio { get; set; }

        [Display(Name = "Data Estimada de Término do Teste")]
        public DateTime? DataEstimada { get; set; }

        public int TestersId { get; set; }

        public DateTime? DataReal { get; set; }

        public string Titulo
        {
            get
            {
                return Id != 0 ? "Editar Task" : "Nova Task";
            }
        }

        public TaskViewModel()
        {
            Id = 0;
        }

        public TaskViewModel(Tasks task)
        {
            Id = task.ProjetosId;
            NomeTask = task.NomeTask;
            DataInicio = task.DataInicio;
            DataEstimada = task.DataEstimada;
            TestersId = task.TestersId;
            ProjetosId = task.ProjetosId;

        }

    }
}