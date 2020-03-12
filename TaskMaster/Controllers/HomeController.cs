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
    [Authorize]
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
            var qtdbugsEmTrat = _context.Bugs.Where(i => i.EstadosBug.NomeEstado == "Em Tratamento").Count().ToString();
            var qtdbugsCorrigido = _context.Bugs.Where(i => i.EstadosBug.NomeEstado == "Corrigido").Count().ToString();

            var bugtipo500 = _context.Bugs.Where(i => i.TiposBugs.TipoBug == "Erro 500").Count().ToString();
            var bugtipo404 = _context.Bugs.Where(i => i.TiposBugs.TipoBug == "Erro 404").Count().ToString();
            var bugtipoInterface = _context.Bugs.Where(i => i.TiposBugs.TipoBug == "Interface").Count().ToString();
            var bugtipoFluxo = _context.Bugs.Where(i => i.TiposBugs.TipoBug == "Fluxo").Count().ToString();
            var bugtipoCalc =  _context.Bugs.Where(i => i.TiposBugs.TipoBug == "Calculo").Count().ToString();

            ViewData["QtdBugsAbertos"] = qtdbugsAberto;
            ViewData["QtdBugsEmTrat"] = qtdbugsEmTrat;
            ViewData["QtdBugsCorrigido"] = qtdbugsCorrigido;

            ViewData["bugtipo500"] = bugtipo500;
            ViewData["bugtipo404"] = bugtipo404;
            ViewData["bugtipoInterface"] = bugtipoInterface;
            ViewData["bugtipoFluxo"] = bugtipoFluxo;
            ViewData["bugtipoCalc"] = bugtipoCalc;

            var viewModel = new BugsViewModel
            {
                Tasks = _context.Tasks.ToList(),
                Projetos = _context.Projetos.ToList(),
                Devs = _context.Devs.ToList(),
                TiposBugs = _context.TiposBugs.ToList(),
                EstadosBugs = _context.EstadosBugs.ToList(),
                Bugs = _context.Bugs.Include(e=>e.EstadosBug).ToList()
              
            };
            return View(viewModel);
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
        }

        public ActionResult ListarBugTipo(string tipobug)
        {
            var tiposbug = _context.Bugs.Where(n => n.TiposBugs.TipoBug == tipobug)
                .Include(t => t.Tasks)
                .Include(p => p.Devs)
                .Include(p => p.Tasks.Projetos)
                .Include(e => e.EstadosBug)
                .Include(t => t.TiposBugs)
                .ToList();

            var bugTipo = _context.Bugs
               .Where(n => n.TiposBugs.TipoBug == tipobug)
               .Select(n => n.TiposBugs.TipoBug)
               .FirstOrDefault();
            ViewData["bugTipo"] = bugTipo;

            return View("ListarBugTipo", tiposbug);
        }

    }
}