using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskMaster.Models;
using TaskMaster.ViewModels;

namespace TaskMaster.Controllers
{
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

        public ViewResult NovoBug()
        {
            var tasks = _context.Tasks.ToList();
            var devs = _context.Devs.ToList();
            var projs = _context.Projetos.ToList();
            var tiposbugs = _context.TiposBugs.ToList();
            var estadosbugs = _context.EstadosBugs.ToList();
            var viewModel = new BugsViewModel
            {
                Tasks = tasks,
                Devs = devs,
                Projetos = projs,
                TiposBugs = tiposbugs,
                EstadosBugs = estadosbugs

            };

            return View("FormBug", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Salvar(Bugs bugs)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new BugsViewModel(bugs)
                {
                    Tasks = _context.Tasks.ToList(),
                    Devs = _context.Devs.ToList(),
                    TiposBugs=_context.TiposBugs.ToList(),
                    EstadosBugs =_context.EstadosBugs.ToList()
                };

                return View("FormBug", viewModel);
            }

            if (bugs.BugsId == 0)
                _context.Bugs.Add(bugs);
            else
            {
                var bugInDb = _context.Bugs.Single(p => p.BugsId == bugs.BugsId);
                bugInDb.DescBug = bugs.DescBug;
                bugInDb.DataBug = bugs.DataBug;
                bugInDb.DataEstimada = bugs.DataBug;
                bugInDb.TasksId = bugs.TasksId;
                bugInDb.TiposBugsId = bugs.TiposBugsId;
                bugInDb.EstadosBugId = bugs.EstadosBugId;
                bugInDb.UrlRepoCodigo = bugs.UrlRepoCodigo;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Bugs");
        }


        public ViewResult Index()
        {
            var bugs = _context.Bugs
                .Include(p => p.Tasks.Projetos)
                .Include(g => g.Tasks)
                .Include(g => g.Devs)
                .Include(b=>b.TiposBugs)
                .Include(e=>e.EstadosBug)
                .ToList();

            return View(bugs);
        }


        public ActionResult Editar(int id)
        {
            var bug = _context.Bugs.SingleOrDefault(c => c.BugsId == id);

            if (bug == null)
                return HttpNotFound();

            var viewModel = new BugsViewModel(bug)
            {
                Tasks = _context.Tasks.ToList(),
                Devs= _context.Devs.ToList(),
                TiposBugs=_context.TiposBugs.ToList(),
                EstadosBugs=_context.EstadosBugs.ToList()
            };

            return View("FormBug", viewModel);
        }


        public ActionResult BugsTask(int id)
        {
            var bugsdatask = _context.Bugs.Where(n => n.TasksId == id)
                .Include(t => t.Tasks)
                .Include(p => p.Devs)
                .Include(p => p.Tasks.Projetos)
                .ToList();

            return View("BugsTask", bugsdatask);
        }

        public ActionResult DetalhesBug(int id)
        {
            var bug = _context.Bugs.SingleOrDefault(c => c.BugsId == id);

            if (bug == null)
                return HttpNotFound();

            var viewModel = new BugsViewModel(bug)
            {
                Tasks = _context.Tasks.ToList(),
                Devs = _context.Devs.ToList(),
                TiposBugs = _context.TiposBugs.ToList(),
                EstadosBugs = _context.EstadosBugs.ToList()
            };

            return View("DetalhesBug", viewModel);
        }

    }
}