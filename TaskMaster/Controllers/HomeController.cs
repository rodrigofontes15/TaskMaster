using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskMaster.Models;
using TaskMaster.ViewModels;

namespace TaskMaster.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ViewResult Index()
        {

            var qtdbugsAberto = _context.Bugs.Where(i=>i.EstadosBug.NomeEstado=="Aberto").Count().ToString();
            ViewData["QtdBugsAbertos"] = qtdbugsAberto;

            var qtdbugsEmTrat = _context.Bugs.Where(i => i.EstadosBug.NomeEstado == "Em Tratamento").Count().ToString();
            ViewData["QtdBugsEmTrat"] = qtdbugsEmTrat;

            var qtdbugsCorrigido = _context.Bugs.Where(i => i.EstadosBug.NomeEstado == "Corrigido").Count().ToString();
            ViewData["QtdBugsCorrigido"] = qtdbugsCorrigido;

            var bugs = _context.Bugs
               .Include(p => p.Tasks.Projetos)
               .Include(g => g.Tasks)
               .Include(g => g.Devs)
               .Include(b => b.TiposBugs)
               .Include(e => e.EstadosBug)
               .ToList();

            return View(bugs);
        }

        public ActionResult ListarBugEstado(string estado)
        {
            var bugestado = _context.Bugs.Where(n => n.EstadosBug.NomeEstado == estado)
                .Include(t => t.Tasks)
                .Include(p => p.Devs)
                .Include(p => p.Tasks.Projetos)
                .Include(e => e.EstadosBug)
                .Include(t => t.TiposBugs)
                .ToList();

            var bugEstado = _context.Bugs
               .Where(n => n.EstadosBug.NomeEstado == estado)
               .Select(n => n.EstadosBug.NomeEstado)
               .FirstOrDefault();
            ViewData["EstadoBug"] = bugEstado;

            return View("ListarBugEstado", bugestado);

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}