using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskMaster.Models;
using TaskMaster.ViewModels;
using Highsoft.Web.Mvc.Charts;

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
            List<PieSeriesData> pieData = new List<PieSeriesData>();

            pieData.Add(new PieSeriesData { Name = "Erro 500", Y = _context.Bugs.Where(i=>i.TiposBugsId == 1).Count()});
            pieData.Add(new PieSeriesData { Name = "Erro 404", Y = _context.Bugs.Where(i => i.TiposBugsId == 2).Count() });
            pieData.Add(new PieSeriesData { Name = "Interface", Y = _context.Bugs.Where(i => i.TiposBugsId == 5).Count() });
            pieData.Add(new PieSeriesData { Name = "Fluxo", Y = _context.Bugs.Where(i => i.TiposBugsId == 6).Count() });
            pieData.Add(new PieSeriesData { Name = "Calculo", Y = _context.Bugs.Where(i => i.TiposBugsId == 7).Count() });

            ViewData["pieData"] = pieData;



            List<double> tokyoValues = new List<double> { 7.0, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2, 26.5, 23.3, 18.3, 13.9, 9.6 };
            List<double> berlinValues = new List<double> { -0.9, 0.6, 3.5, 8.4, 13.5, 17.0, 18.6, 17.9, 14.3, 9.0, 3.9, 1.0 };
            List<double> londonValues = new List<double> { 3.9, 4.2, 5.7, 8.5, 11.9, 15.2, 17.0, 16.6, 14.2, 10.3, 6.6, 4.8 };
            List<LineSeriesData> tokyoData = new List<LineSeriesData>();
            List<LineSeriesData> berlinData = new List<LineSeriesData>();
            List<LineSeriesData> londonData = new List<LineSeriesData>();

            tokyoValues.ForEach(p => tokyoData.Add(new LineSeriesData { Y = p }));
            berlinValues.ForEach(p => berlinData.Add(new LineSeriesData { Y = p }));
            londonValues.ForEach(p => londonData.Add(new LineSeriesData { Y = p }));

            ViewData["tokyoData"] = tokyoData;
            ViewData["berlinData"] = berlinData;
            ViewData["londonData"] = londonData;

            var qtdbugsAberto = _context.Bugs.Where(i=>i.EstadosBug.NomeEstado=="Aberto").Count().ToString();
            var qtdbugsEmTrat = _context.Bugs.Where(i => i.EstadosBug.NomeEstado == "Em Tratamento").Count().ToString();
            var qtdbugsCorrigido = _context.Bugs.Where(i => i.EstadosBug.NomeEstado == "Corrigido").Count().ToString();

            var qtdprojsNoPrazo = _context.Projetos.Where(i => i.EstadoProj == "No Prazo").Count().ToString();
            var qtdprojsEmAtraso = _context.Projetos.Where(i => i.EstadoProj == "Em Atraso").Count().ToString();

            var bugtipo500 = _context.Bugs.Where(i => i.TiposBugs.TipoBug == "Erro 500").Count().ToString();
            var bugtipo404 = _context.Bugs.Where(i => i.TiposBugs.TipoBug == "Erro 404").Count().ToString();
            var bugtipoInterface = _context.Bugs.Where(i => i.TiposBugs.TipoBug == "Interface").Count().ToString();
            var bugtipoFluxo = _context.Bugs.Where(i => i.TiposBugs.TipoBug == "Fluxo").Count().ToString();
            var bugtipoCalc =  _context.Bugs.Where(i => i.TiposBugs.TipoBug == "Calculo").Count().ToString();

            var temposolucao = _context.Bugs.Select(t=>t.TempoSolucao).FirstOrDefault();
            if (temposolucao == null)
            {
                var MediaTempoSolucao = 0;
                ViewData["MediaTempoSolucao"] = MediaTempoSolucao;
            }
            else
            {
                var MediaTempoSolucao = _context.Bugs.Average(t => t.TempoSolucao).Value.ToString("0", System.Globalization.CultureInfo.InvariantCulture);
                ViewData["MediaTempoSolucao"] = MediaTempoSolucao;
            }

            ViewData["QtdBugsAbertos"] = qtdbugsAberto;
            ViewData["QtdBugsEmTrat"] = qtdbugsEmTrat;
            ViewData["QtdBugsCorrigido"] = qtdbugsCorrigido;

            ViewData["qtdprojsNoPrazo"] = qtdprojsNoPrazo;
            ViewData["qtdprojsEmAtraso"] = qtdprojsEmAtraso;

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
                Bugs = _context.Bugs.Include(e => e.EstadosBug).Include(t => t.Tasks).Include(p => p.Tasks.Projetos).ToList(),
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

        public ActionResult ListarBugDev(int DevId)
        {
            var bugedev = _context.Bugs.Where(n => n.DevsId == DevId)
                .Include(t => t.Tasks)
                .Include(p => p.Devs)
                .Include(p => p.Tasks.Projetos)
                .Include(e => e.EstadosBug)
                .Include(t => t.TiposBugs)
                .ToList();

            var bugNomeDev = _context.Bugs
               .Where(n => n.Devs.DevsId == DevId)
               .Select(n => n.Devs.DevNome)
               .FirstOrDefault();
            ViewData["bugNomeDev"] = bugNomeDev;

            return View("ListarBugDev", bugedev);
        }


        public ActionResult ListaProjetoEstado(string estado)
        {
            var estadoprojeto = _context.Projetos.Where(n => n.EstadoProj == estado)
               .Include(g => g.GerenteProjs)
                .ToList();

            var prjEstado = _context.Projetos
               .Where(n => n.EstadoProj == estado)
               .Select(n => n.EstadoProj)
               .FirstOrDefault();
            ViewData["prjEstado"] = prjEstado;

            return View("ListaProjetoEstado", estadoprojeto);
        }
    }
}