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
        [Display(Name = "Projeto Relacionado")]
        public int ProjetosId { get; set; }

        public IEnumerable<Testers> Testers { get; set; }
        public string Nome { get; set; }
        [Display(Name = "Assignar para o Testador")]
        public int TestersId { get; set; }

        public IEnumerable<TiposTestes> TiposTestes { get; set; }
        [Display(Name = "Tipo de Teste")]
        public int TiposTestesId { get; set; }
        public string TipoTeste { get; set; }

        public int? TasksId {get; set;}

        [Required]
        [StringLength(255)]
        [Display(Name = "Tarefa de Teste")]
        public string NomeTask { get; set; }

        [Display(Name = "Data de Início do Teste")]
        public DateTime? DataInicio { get; set; }

        [Display(Name = "Data Estimada de Término do Teste")]
        public DateTime? DataEstimada { get; set; }

        [Display(Name = "Estado da Task")]
        public string EstadoTask { get; set; }

        public DateTime? DataReal { get; set; }

        public string Titulo
        {
            get
            {
                return TasksId != 0 ? "Editar Task" : "Nova Task";
            }
        }

        public TaskViewModel()
        {
            TasksId = 0;
        }

        public TaskViewModel(Tasks task)
        {
            TasksId = task.TasksId;
            NomeTask = task.NomeTask;
            DataInicio = task.DataInicio;
            DataEstimada = task.DataEstimada;
            TestersId = task.TestersId;
            ProjetosId = task.ProjetosId;
            TestersId = task.TestersId;
            TiposTestesId = task.TiposTestesId;
        }

    }
}