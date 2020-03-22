using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskMaster.Models;
using TaskMaster.ViewModels;

namespace TaskMaster.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private ApplicationDbContext _context;

        public TasksController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ViewResult Index()
        {
            var tasks = _context.Tasks
                .Include(g => g.Projetos)
                .Include(g => g.Testers)
                .Include(t => t.TiposTestes)
                .ToList();
            if (User.IsInRole("tester"))
            {
                return View("Index", tasks);
            }
            else
            {
                if (User.IsInRole("admin"))
                {
                    return View("Index", tasks);
                }
                else
                    return View("Index_SomenteLeitura", tasks);
            }
        }

        [Authorize(Roles = NomeRoles.tester + "," + NomeRoles.admin)]
        public ViewResult NovaTask()
        {
            var projetos = _context.Projetos.ToList();
            var testers = _context.Testers.ToList();
            var tipostestes = _context.TiposTestes.ToList();
            var viewModel = new TaskViewModel
            {
                Testers = testers,
                Projetos = projetos,
                TiposTestes = tipostestes
            };

            return View("FormTask", viewModel);
        }

        [Authorize(Roles = NomeRoles.tester + "," + NomeRoles.admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Salvar(Tasks task)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new TaskViewModel(task)
                {
                    Testers = _context.Testers.ToList(),
                    Projetos = _context.Projetos.ToList(),
                    TiposTestes = _context.TiposTestes.ToList()
                };

                return View("FormTask", viewModel);
            }

            if (task.TasksId == 0)
            {
                var ProjetosId = task.ProjetosId;
                var estadoProjetoinDb = _context.Projetos.Where(i => i.ProjetosId == ProjetosId).Select(e => e.EstadoProj).FirstOrDefault();
                if (estadoProjetoinDb == "Fechado")
                {
                    return Content("Projeto Fechado");
                }
                else
                    _context.Tasks.Add(task);
            }
            else
            {   
                var taskInDb = _context.Tasks.Single(p => p.TasksId == task.TasksId);
                taskInDb.NomeTask = task.NomeTask;
                taskInDb.DataInicio = task.DataInicio;
                taskInDb.DataEstimada = task.DataEstimada;
                taskInDb.TestersId = task.TestersId;
                taskInDb.ProjetosId = task.ProjetosId;
                taskInDb.TiposTestesId = task.TiposTestesId;

                var estadoProjetoinDb = _context.Projetos.Where(i => i.ProjetosId == taskInDb.ProjetosId).Select(e => e.EstadoProj).FirstOrDefault();
                if (estadoProjetoinDb == "Fechado")
                {
                    return Content("Projeto Fechado");
                }
            }
            _context.SaveChanges();

            var taskid = _context.Tasks.Where(p => p.TasksId == task.TasksId).Select(t => t.ProjetosId).SingleOrDefault();
            var projetid = _context.Projetos.Where(t => t.ProjetosId == taskid).Select(p => p.ProjetosId).SingleOrDefault();
            var estadoprojeto = _context.Projetos.Where(t => t.ProjetosId == taskid).Select(p => p.EstadoProj).SingleOrDefault();

            var sqlQtdTaskPrj = @"Update [Projetos] SET QtdTasksPrj = (QtdTasksPrj+1) WHERE ProjetosId = @ProjetosId";
            _context.Database.ExecuteSqlCommand(
                sqlQtdTaskPrj,
                new SqlParameter("@ProjetosId", projetid));

            var taskIdEstado = _context.Tasks.Where(c => c.TasksId == task.TasksId).Select(c => c.TasksId).SingleOrDefault();

            var sqlEstadoTask = @"Update [Tasks] SET EstadoTask = 'Em Andamento' WHERE TasksId = @TasksId";
            _context.Database.ExecuteSqlCommand(
                sqlEstadoTask,
                new SqlParameter("@TasksId", taskIdEstado));

            if (estadoprojeto == "Fechado")
            {
                var sqlEstadoPrj = @"Update [Projetos] SET EstadoProj = ' ' WHERE ProjetosId = @ProjetosId";
                _context.Database.ExecuteSqlCommand(
                    sqlEstadoPrj,
                    new SqlParameter("@ProjetosId", projetid));

                return RedirectToAction("Index", "Tasks");
            }

            return RedirectToAction("Index", "Tasks");
        }

        [Authorize(Roles = NomeRoles.tester + "," + NomeRoles.admin)]
        public ActionResult Editar(int id)
        {
            var task = _context.Tasks.SingleOrDefault(c => c.TasksId == id);

            if (task == null)
                return HttpNotFound();

            var viewModel = new TaskViewModel(task)
            {
                Projetos = _context.Projetos.ToList(),
                Testers = _context.Testers.ToList(),
                TiposTestes = _context.TiposTestes.ToList()
            };

            return View("FormTask", viewModel);
        }


          public ActionResult TasksProjeto(int id)
        {
            var tasksdoprojeto = _context.Tasks.Where(n => n.ProjetosId == id)
                .Include(t => t.Testers)
                .Include(p => p.Projetos)
                .Include(t => t.TiposTestes)
                .ToList();

            var nomeProjeto = _context.Tasks
                .Where(n => n.ProjetosId == id)
                .Select(n => n.Projetos.NomeProjeto)
                .FirstOrDefault();
            ViewData["NomeProjeto"] = nomeProjeto;

            return View("TasksProjeto", tasksdoprojeto);    
        }

        public ActionResult Detalhes(int id)
        {
            var task = _context.Tasks.Where(c => c.TasksId == id)
                .Include(g => g.Projetos)
                .Include(g => g.Testers)
                .Include(t=>t.TiposTestes)
                .ToList(); ;

            if (task == null)
                return HttpNotFound();

            return View(task);
        }

        [Authorize(Roles = NomeRoles.tester + "," + NomeRoles.admin)]
        public ActionResult FecharTask(int id)
        {
            var bugsabertos = _context.Bugs.Where(c => c.TasksId == id).Count(e=>e.EstadosBugId==2);
            var bugsemandamento = _context.Bugs.Where(c => c.TasksId == id).Count(e => e.EstadosBugId==3);

            if (bugsabertos!=0)
            {
                return Content("Task Contem Bugs Abertos");
            }
            else if (bugsemandamento != 0)
            {
                return Content("Task Contem Bugs Em Tratamento");
            }
            else
            {
                var taskInDb = _context.Tasks.Where(c => c.TasksId == id).Select(c=>c.TasksId).FirstOrDefault();

                var sqlEstadoTask = @"Update [Tasks] SET EstadoTask = 'Fechado' WHERE TasksId = @TasksId";
                _context.Database.ExecuteSqlCommand(
                    sqlEstadoTask,
                    new SqlParameter("@TasksId", taskInDb));

                return Redirect(Request.UrlReferrer.ToString());
            }
        }

    }
}