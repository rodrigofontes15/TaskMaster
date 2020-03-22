using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskMaster.Models;
using TaskMaster.ViewModels;

namespace TaskMaster.Controllers
{
    [Authorize]
    public class BugsController : Controller
    {
        private ApplicationDbContext _context;

        public BugsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ViewResult Index()
        {
            var bugs = _context.Bugs
                .Include(p => p.Tasks.Projetos)
                .Include(g => g.Tasks)
                .Include(g => g.Devs)
                .Include(b => b.TiposBugs)
                .Include(e => e.EstadosBug)
                .ToList();
            if (User.IsInRole("dev"))
            {
                return View("Index", bugs);
            }
            else
            {
                if (User.IsInRole("admin"))
                {
                    return View("Index", bugs);
                }
                else
                    return View("Index_SomenteLeitura", bugs);
            }
        }

        [Authorize(Roles = NomeRoles.tester + "," + NomeRoles.admin)]
        public ActionResult FormBug()
        {
            //  List<Projetos> ListaProjetos = _context.Projetos.ToList();
            //ViewBag.ListaProjetos = new SelectList(ListaProjetos, "ProjetosId", "NomeProjeto");

            var viewModel = new BugsViewModel
            {
                Tasks = _context.Tasks.ToList(),
                Devs = _context.Devs.ToList(),
                Projetos = _context.Projetos.ToList(),
                TiposBugs = _context.TiposBugs.ToList(),
                EstadosBugs = _context.EstadosBugs.ToList()
        };

            return View("FormBug", viewModel);
        }

        public JsonResult ListarTasksProjeto(int ProjetosId)
        {

            List<Tasks> ListaTasks = _context.Tasks.Where(p => p.ProjetosId == ProjetosId).ToList();
            return Json(ListaTasks, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = NomeRoles.dev + "," + NomeRoles.admin + "," + NomeRoles.tester)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Salvar(Bugs bugs)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new BugsViewModel(bugs)
                {
                    Projetos = _context.Projetos.ToList(),
                    Tasks = _context.Tasks.ToList(),
                    Devs = _context.Devs.ToList(),
                    TiposBugs = _context.TiposBugs.ToList(),
                    EstadosBugs = _context.EstadosBugs.ToList()
                };

                return View("FormBug", viewModel);
            }
            if (bugs.BugsId == 0)
            {
                var TasksId = bugs.TasksId;
                var estadoTaskinDb = _context.Tasks.Where(i => i.TasksId == TasksId).Select(e => e.EstadoTask).FirstOrDefault();
                if (estadoTaskinDb == "Fechado")
                {
                    return Content("Task Fechada!");
                }
                else
                    _context.Bugs.Add(bugs);
            }
            else
            {
                var bugInDb = _context.Bugs.Single(p => p.BugsId == bugs.BugsId);
                bugInDb.DescBug = bugs.DescBug;
                bugInDb.DataBug = bugs.DataBug;
                bugInDb.DataEstimadaBug = bugs.DataEstimadaBug;
                //bugInDb.ProjetosId = bugs.ProjetosId;
                bugInDb.TasksId = bugs.TasksId;
                bugInDb.DevsId = bugs.DevsId;
                bugInDb.TiposBugsId = bugs.TiposBugsId;
                bugInDb.EstadosBugId = bugs.EstadosBugId;
                bugInDb.UrlRepoCodigo = bugs.UrlRepoCodigo;

                var estadoTaskinDb = _context.Tasks.Where(i => i.TasksId == bugInDb.TasksId).Select(e => e.EstadoTask).FirstOrDefault();
                if (estadoTaskinDb == "Fechado")
                {
                    return Content("Task Fechada!");
                }

                if (bugInDb.DataEstimadaBug == null)
                {
                    bugInDb.DataEstimadaBug = bugs.DataBug.Value.AddDays(5);
                }
                else if (bugInDb.DataEstimadaBug < bugs.DataBug)
                {
                    return Content("Data estimada não pode ser menor que data do Bug");
                }
                else
                {
                    bugInDb.TempoSolucao = (bugs.DataEstimadaBug - bugs.DataBug).Value.TotalDays;
                }
            }
            _context.SaveChanges();

            var taskdobug = _context.Bugs.Where(p => p.BugsId == bugs.BugsId).Select(t => t.TasksId).SingleOrDefault();

            var sqlQtdBugsTask = @"Update [Tasks] SET QtdBugsTsk = (QtdBugsTsk+1) WHERE TasksId = @TasksId";
            _context.Database.ExecuteSqlCommand(
                sqlQtdBugsTask,
                new SqlParameter("@TasksId", taskdobug));

            var projetidnatask = _context.Tasks.Where(t => t.TasksId == taskdobug).Select(p => p.ProjetosId).SingleOrDefault();

            var sqlQtdBugsPrj = @"Update [Projetos] SET QtdBugsPrj = (QtdBugsPrj+1) WHERE ProjetosId = @ProjetosId";
            _context.Database.ExecuteSqlCommand(
                sqlQtdBugsPrj,
                new SqlParameter("@ProjetosId", projetidnatask));

            var sqlRatioBugsPrj = @"Update [Projetos] SET BugsRatio = (QtdBugsPrj/QtdTasksPrj) WHERE ProjetosId = @ProjetosId";
            _context.Database.ExecuteSqlCommand(
                sqlRatioBugsPrj,
                new SqlParameter("@ProjetosId", projetidnatask));

            var sqlEstadoTask = @"Update [Tasks] SET EstadoTask = 'Em Andamento' WHERE TasksId = @TasksId";
            _context.Database.ExecuteSqlCommand(
                sqlEstadoTask,
                new SqlParameter("@TasksId", taskdobug));

            return RedirectToAction("Index", "Bugs");

        }

        [Authorize(Roles = NomeRoles.dev + "," + NomeRoles.admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SalvarBug(Bugs bugs)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new BugsViewModel(bugs)
                {
                    Projetos = _context.Projetos.ToList(),
                    Tasks = _context.Tasks.ToList(),
                    Devs = _context.Devs.ToList(),
                    TiposBugs = _context.TiposBugs.ToList(),
                    EstadosBugs = _context.EstadosBugs.ToList()
                };
                return View("FormBug", viewModel);
            }
            else
            {
                var bugInDb = _context.Bugs.Single(p => p.BugsId == bugs.BugsId);
                bugInDb.DescBug = bugs.DescBug;
                bugInDb.DataBug = bugs.DataBug;
                bugInDb.DataEstimadaBug = bugs.DataEstimadaBug;
                //bugInDb.ProjetosId = bugs.ProjetosId;
                bugInDb.TasksId = bugs.TasksId;
                bugInDb.DevsId = bugs.DevsId;
                bugInDb.TiposBugsId = bugs.TiposBugsId;
                bugInDb.EstadosBugId = bugs.EstadosBugId;
                bugInDb.UrlRepoCodigo = bugs.UrlRepoCodigo;

                if (bugInDb.DataEstimadaBug == null)
                {
                    bugInDb.DataEstimadaBug = bugs.DataBug.Value.AddDays(5);
                }
                else if (bugInDb.DataEstimadaBug < bugs.DataBug)
                {
                    return Content("Data estimada não pode ser menor que data do Bug");
                }
                else
                {
                    bugInDb.TempoSolucao = (bugs.DataEstimadaBug - bugs.DataBug).Value.TotalDays;
                }
            }
            _context.SaveChanges();

            var taskdobug = _context.Bugs.Where(p => p.BugsId == bugs.BugsId).Select(t => t.TasksId).SingleOrDefault();
            var datasbugnatask = _context.Bugs.Where(i => i.TasksId == taskdobug).Select(d => d.DataEstimadaBug).ToList().Max();

            if (datasbugnatask == null)
            {
                datasbugnatask = bugs.DataBug;
            }
            var sqlDataRealTask = @"Update [Tasks] SET DataReal = @DataReal WHERE TasksId = @TasksId";
            _context.Database.ExecuteSqlCommand(
                sqlDataRealTask,
                new SqlParameter("@DataReal", datasbugnatask),
                new SqlParameter("@TasksId", taskdobug));

            var projetidnatask = _context.Tasks.Where(t => t.TasksId == taskdobug).Select(p => p.ProjetosId).SingleOrDefault();
            var datarealnatask = _context.Tasks.Where(i => i.ProjetosId == projetidnatask).Select(d => d.DataReal).ToList().Max();

            if (datarealnatask == null)
            {
                datarealnatask = bugs.DataEstimadaBug;
            }
            var sqlDataRealProjeto = @"Update [Projetos] SET DataReal = @DataReal WHERE ProjetosId = @ProjetosId";

            _context.Database.ExecuteSqlCommand(
                sqlDataRealProjeto,
                new SqlParameter("@DataReal", datarealnatask),
                new SqlParameter("@ProjetosId", projetidnatask));

            var sqlRatioBugsPrj = @"Update [Projetos] SET BugsRatio = (QtdBugsPrj/QtdTasksPrj) WHERE ProjetosId = @ProjetosId";
            _context.Database.ExecuteSqlCommand(
                sqlRatioBugsPrj,
                new SqlParameter("@ProjetosId", projetidnatask));

            var dataestimadanoprojeto = _context.Tasks.Where(i => i.ProjetosId == projetidnatask).Select(d => d.Projetos.DataEstimada).FirstOrDefault();
            var datarealnoprojeto = _context.Tasks.Where(i => i.ProjetosId == projetidnatask).Select(d => d.Projetos.DataReal).FirstOrDefault();

            if (dataestimadanoprojeto < datarealnoprojeto)
            {
                var sqlDataPrj = @"Update [Projetos] SET EstadoProj = 'Em Atraso'  WHERE ProjetosId = @ProjetosId";
                _context.Database.ExecuteSqlCommand(
                    sqlDataPrj,
                    new SqlParameter("@ProjetosId", projetidnatask));
            }
            else
            {
                var sqlDataPrj = @"Update [Projetos] SET EstadoProj = 'No Prazo'  WHERE ProjetosId = @ProjetosId";
                _context.Database.ExecuteSqlCommand(
                    sqlDataPrj,
                    new SqlParameter("@ProjetosId", projetidnatask));
            }

            return Redirect(Request.UrlReferrer.ToString());

        }

        [Authorize(Roles = NomeRoles.tester + "," + NomeRoles.admin)]
        public ActionResult Editar(int id)
        {
            var bug = _context.Bugs.SingleOrDefault(c => c.BugsId == id);

            if (bug == null)
                return HttpNotFound();

            var viewModel = new BugsViewModel(bug)
            {
                Tasks = _context.Tasks.ToList(),
                Projetos = _context.Projetos.ToList(),
                Devs = _context.Devs.ToList(),
                TiposBugs = _context.TiposBugs.ToList(),
                EstadosBugs = _context.EstadosBugs.ToList()
            };

            return View("FormBug", viewModel);
        }

        public ActionResult BugsTask(int id)
        {
            var bugsdatask = _context.Bugs.Where(n => n.TasksId == id)
                .Include(t => t.Tasks)
                .Include(p => p.Devs)
                .Include(p => p.Tasks.Projetos)
                .Include(e => e.EstadosBug)
                .Include(t => t.TiposBugs)
                .ToList();

            var nomeTask = _context.Bugs
                .Where(n => n.Tasks.TasksId == id)
                .Select(n => n.Tasks.NomeTask)
                .FirstOrDefault();
            ViewData["NomeTask"] = nomeTask;

            return View("BugsTask", bugsdatask);
        }

        public ActionResult DetalhesBug(int id)
        {
            var bug = _context.Bugs.SingleOrDefault(c => c.BugsId == id);
            var taskIdBug = _context.Bugs.Where(t => t.BugsId == id).Select(t => t.TasksId).FirstOrDefault();
            var projetoIdtask = _context.Tasks.Where(t => t.TasksId == taskIdBug).Select(p => p.ProjetosId).FirstOrDefault();
            var devIdBug = _context.Bugs.Where(t => t.BugsId == id).Select(p => p.DevsId).FirstOrDefault();
            var estadoIdBug = _context.Bugs.Where(t => t.BugsId == id).Select(p => p.EstadosBugId).FirstOrDefault();
            var tipoIdBug = _context.Bugs.Where(t => t.BugsId == id).Select(p => p.TiposBugsId).FirstOrDefault();

            if (bug == null)
                return HttpNotFound();

            var viewModel = new BugsViewModel(bug)
            {
                Tasks = _context.Tasks.ToList(),
                Projetos = _context.Projetos.ToList(),
                Devs = _context.Devs.ToList(),
                TiposBugs = _context.TiposBugs.ToList(),
                EstadosBugs = _context.EstadosBugs.ToList(),
                NotasTrabalhoBugs=_context.NotasTrabalhoBug.Where(c=>c.BugsId==id).ToList()
            };

            var nomeTask = _context.Bugs
               .Where(n => n.TasksId == taskIdBug)
             .Select(n => n.Tasks.NomeTask)
           .FirstOrDefault();
            ViewData["NomeTask"] = nomeTask;

            var nomeProjeto = _context.Tasks
              .Where(n => n.ProjetosId == projetoIdtask)
            .Select(n => n.Projetos.NomeProjeto)
          .FirstOrDefault();
            ViewData["NomeProjeto"] = nomeProjeto;

            var nomeDev = _context.Bugs
              .Where(n => n.DevsId == devIdBug)
            .Select(n => n.Devs.DevNome)
          .FirstOrDefault();
            ViewData["NomeDev"] = nomeDev;

            var estadoBug = _context.Bugs
             .Where(n => n.EstadosBugId == estadoIdBug)
            .Select(n => n.EstadosBug.NomeEstado)
            .FirstOrDefault();
            ViewData["NomeEstado"] = estadoBug;

            var nomeTipoBug = _context.Bugs
              .Where(n => n.TiposBugsId == tipoIdBug)
            .Select(n => n.TiposBugs.TipoBug)
          .FirstOrDefault();
            ViewData["nomeTipoBug"] = nomeTipoBug;

            if (User.IsInRole("dev"))
            {
                return View("DetalhesBug", viewModel);
            }
            else
            {
                if (User.IsInRole("admin"))
                {
                    return View("DetalhesBug", viewModel);
                }
                else
                    return View("DetalhesBug_SomenteLeitura", viewModel);
            }
            
        }

        [Authorize(Roles = NomeRoles.dev + "," + NomeRoles.admin)]
        public ActionResult EmTratamento(int id)
        {
            var dataestimada = _context.Bugs.Where(i => i.BugsId == id).Select(d => d.DataEstimadaBug).Single();
            if (dataestimada == null)
            {
                return Content("Defina a Data Estimada!");
            }
            else
            { 
            var datahoraagora = DateTime.Now;
            string nota = "Bug passado para -Em Tratamento- nesta data";

            var sqlEstadoBug = @"Update [Bugs] SET EstadosBugId = (3) WHERE BugsId = @BugsId";
            _context.Database.ExecuteSqlCommand(
                sqlEstadoBug,
                new SqlParameter("@BugsId", id));

            var sqlNotaUpdate = @"INSERT INTO NotasTrabalhoBugs (DataNotaTrabalho, NotasTrabalho, BugsId) VALUES (@datahoraagora,@nota,@BugsId)";
            _context.Database.ExecuteSqlCommand(
                sqlNotaUpdate,
                new SqlParameter("@BugsId", id),
                new SqlParameter("@datahoraagora", datahoraagora),
                new SqlParameter("@nota", nota));


            return Redirect(Request.UrlReferrer.ToString());
                }
        }

        [Authorize(Roles = NomeRoles.dev + "," + NomeRoles.admin)]
        public ActionResult Corrigido(int id)
        {
            var datahoraagora = DateTime.Now;
            string nota = "Bug Corrigido nesta data";

            var sqlEstadoBug = @"Update [Bugs] SET EstadosBugId = (4) WHERE BugsId = @BugsId";
            _context.Database.ExecuteSqlCommand(
                sqlEstadoBug,
                new SqlParameter("@BugsId", id));

            var sqlNotaUpdate = @"INSERT INTO NotasTrabalhoBugs (DataNotaTrabalho, NotasTrabalho, BugsId) VALUES (@datahoraagora,@nota,@BugsId)";
            _context.Database.ExecuteSqlCommand(
                sqlNotaUpdate,
                new SqlParameter("@BugsId", id),
                new SqlParameter("@datahoraagora", datahoraagora),
                new SqlParameter("@nota", nota));

            var sqlDataRealBug = @"Update [Bugs] SET DataEstimadaBug = @DataEstimadaBug WHERE BugsId = @BugsId";
            _context.Database.ExecuteSqlCommand(
                sqlDataRealBug,
                new SqlParameter("@DataEstimadaBug", datahoraagora),
                new SqlParameter("@BugsId", id));

            var DataEstimadaBug = _context.Bugs.Where(b => b.BugsId == id).Select(d => d.DataEstimadaBug).FirstOrDefault();

            var DataBug = _context.Bugs.Where(b => b.BugsId == id).Select(d => d.DataBug).FirstOrDefault();

            var TempoSolucao = (DataEstimadaBug - DataBug).Value.TotalDays;

            var sqlTempoSolucao = @"Update [Bugs] SET TempoSolucao = @TempoSolucao WHERE BugsId = @BugsId";
            _context.Database.ExecuteSqlCommand(
                sqlTempoSolucao,
                new SqlParameter("@BugsId", id),
                new SqlParameter("@TempoSolucao", TempoSolucao));

            var taskdobug = _context.Bugs.Where(p => p.BugsId == id).Select(t => t.TasksId).SingleOrDefault();
            var datasbugnatask = _context.Bugs.Where(i => i.TasksId == taskdobug).Select(d => d.DataEstimadaBug).ToList().Max();

            var sqlDataRealTask = @"Update [Tasks] SET DataReal = @DataReal WHERE TasksId = @TasksId";
            _context.Database.ExecuteSqlCommand(
                sqlDataRealTask,
                new SqlParameter("@DataReal", datasbugnatask),
                new SqlParameter("@TasksId", taskdobug));

            var projetidnatask = _context.Tasks.Where(t => t.TasksId == taskdobug).Select(p => p.ProjetosId).SingleOrDefault();
            var datarealnatask = _context.Tasks.Where(i => i.ProjetosId == projetidnatask).Select(d => d.DataReal).ToList().Max();

            var sqlDataRealProjeto = @"Update [Projetos] SET DataReal = @DataReal WHERE ProjetosId = @ProjetosId";

            _context.Database.ExecuteSqlCommand(
                sqlDataRealProjeto,
                new SqlParameter("@DataReal", datarealnatask),
                new SqlParameter("@ProjetosId", projetidnatask));

            var dataestimadanoprojeto = _context.Tasks.Where(i => i.ProjetosId == projetidnatask).Select(d => d.Projetos.DataEstimada).FirstOrDefault();
            var datarealnoprojeto = _context.Tasks.Where(i => i.ProjetosId == projetidnatask).Select(d => d.Projetos.DataReal).FirstOrDefault();

            if (dataestimadanoprojeto < datarealnoprojeto)
            {
                var sqlDataPrj = @"Update [Projetos] SET EstadoProj = 'Em Atraso'  WHERE ProjetosId = @ProjetosId";
                _context.Database.ExecuteSqlCommand(
                    sqlDataPrj,
                    new SqlParameter("@ProjetosId", projetidnatask));
            }
            else
            {
                var sqlDataPrj = @"Update [Projetos] SET EstadoProj = 'No Prazo'  WHERE ProjetosId = @ProjetosId";
                _context.Database.ExecuteSqlCommand(
                    sqlDataPrj,
                    new SqlParameter("@ProjetosId", projetidnatask));
            }

            return Redirect(Request.UrlReferrer.ToString());
        }

        [Authorize(Roles = NomeRoles.dev + "," + NomeRoles.admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNotasBug(NotasTrabalhoBug notasTrabalhoBugs)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new BugsViewModel(notasTrabalhoBugs)
                {
                    NotasTrabalhoBugs = _context.NotasTrabalhoBug.ToList()
                };

                return View("Index", viewModel);
            }

            if (notasTrabalhoBugs.NotasTrabalhoBugId == 0)
            { 
                notasTrabalhoBugs.DataNotaTrabalho = DateTime.Now;
                _context.NotasTrabalhoBug.Add(notasTrabalhoBugs);
            }
            else
            {
                var notasInDb = _context.NotasTrabalhoBug
                    .Single(p => p.NotasTrabalhoBugId == notasTrabalhoBugs.NotasTrabalhoBugId);
                notasInDb.NotasTrabalhoBugId = notasTrabalhoBugs.NotasTrabalhoBugId;
                notasInDb.DataNotaTrabalho = notasTrabalhoBugs.DataNotaTrabalho;
            }
            _context.SaveChanges();

            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult NotasBug(int id)
        {
            var notasbug = _context.NotasTrabalhoBug.Where(n => n.BugsId == id)
                .ToList();

            var nomeTask = _context.Bugs
                .Where(n => n.BugsId == id)
                .Select(n => n.Tasks.NomeTask)
                .FirstOrDefault();
            ViewData["NomeTask"] = nomeTask;

            var tipoBug = _context.Bugs
                .Where(n => n.BugsId == id)
                .Select(n => n.TiposBugs.TipoBug)
                .FirstOrDefault();
            ViewData["TipoBug"] = tipoBug;

            if (User.IsInRole("dev"))
            {
                  return View("NotasBug", notasbug);
            }
            else
            {
                if (User.IsInRole("admin"))
                {
                    return View("NotasBug", notasbug);
                }
                else
                    return View("NotasBug_SomenteLeitura", notasbug);
            }
        }

    }

}

