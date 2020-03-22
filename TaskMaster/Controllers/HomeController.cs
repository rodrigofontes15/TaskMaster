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

            var bugsabertosemandamentojan = _context.Bugs.Where(t => t.DataBug.Value.Month == 1).Count();
            var bugsabertosemandamentofeb = _context.Bugs.Where(t => t.DataBug.Value.Month == 2).Count();
            var bugsabertosemandamentomar = _context.Bugs.Where(t => t.DataBug.Value.Month == 3).Count();
            var bugsabertosemandamentoabr = _context.Bugs.Where(t => t.DataBug.Value.Month == 4).Count();
            var bugsabertosemandamentomai = _context.Bugs.Where(t => t.DataBug.Value.Month == 5).Count();
            var bugsabertosemandamentojun = _context.Bugs.Where(t => t.DataBug.Value.Month == 6).Count();
            var bugsabertosemandamentojul = _context.Bugs.Where(t => t.DataBug.Value.Month == 7).Count();
            var bugsabertosemandamentoago = _context.Bugs.Where(t => t.DataBug.Value.Month == 8).Count();
            var bugsabertosemandamentoset = _context.Bugs.Where(t => t.DataBug.Value.Month == 9).Count();
            var bugsabertosemandamentoout = _context.Bugs.Where(t => t.DataBug.Value.Month == 10).Count();
            var bugsabertosemandamentonov = _context.Bugs.Where(t => t.DataBug.Value.Month == 11).Count();
            var bugsabertosemandamentodez = _context.Bugs.Where(t => t.DataBug.Value.Month == 12).Count();

            var bugscorrigidosjan = _context.Bugs.Where(t => t.DataEstimadaBug.Value.Month == 1).Where(b=>b.EstadosBugId==4).Count();
            var bugscorrigidosfev = _context.Bugs.Where(t => t.DataEstimadaBug.Value.Month == 2).Where(b => b.EstadosBugId == 4).Count();
            var bugscorrigidosmar = _context.Bugs.Where(t => t.DataEstimadaBug.Value.Month == 3).Where(b => b.EstadosBugId == 4).Count();
            var bugscorrigidosabr = _context.Bugs.Where(t => t.DataEstimadaBug.Value.Month == 4).Where(b => b.EstadosBugId == 4).Count();
            var bugscorrigidosmai = _context.Bugs.Where(t => t.DataEstimadaBug.Value.Month == 5).Where(b => b.EstadosBugId == 4).Count();
            var bugscorrigidosjun = _context.Bugs.Where(t => t.DataEstimadaBug.Value.Month == 6).Where(b => b.EstadosBugId == 4).Count();
            var bugscorrigidosjul = _context.Bugs.Where(t => t.DataEstimadaBug.Value.Month == 7).Where(b => b.EstadosBugId == 4).Count();
            var bugscorrigidosago = _context.Bugs.Where(t => t.DataEstimadaBug.Value.Month == 8).Where(b => b.EstadosBugId == 4).Count();
            var bugscorrigidosset = _context.Bugs.Where(t => t.DataEstimadaBug.Value.Month == 9).Where(b => b.EstadosBugId == 4).Count();
            var bugscorrigidosout = _context.Bugs.Where(t => t.DataEstimadaBug.Value.Month == 10).Where(b => b.EstadosBugId == 4).Count();
            var bugscorrigidosnov = _context.Bugs.Where(t => t.DataEstimadaBug.Value.Month == 11).Where(b => b.EstadosBugId == 4).Count();
            var bugscorrigidosdez = _context.Bugs.Where(t => t.DataEstimadaBug.Value.Month == 12).Where(b => b.EstadosBugId == 4).Count();

            List<int> encontradoValores = new List<int> { bugsabertosemandamentojan, bugsabertosemandamentofeb,
            bugsabertosemandamentomar, bugsabertosemandamentoabr, bugsabertosemandamentomai, bugsabertosemandamentojun,
            bugsabertosemandamentojul, bugsabertosemandamentoago, bugsabertosemandamentoset, bugsabertosemandamentoout,
            bugsabertosemandamentonov,bugsabertosemandamentodez };
            
            List<int> corrigidosValores = new List<int> { bugscorrigidosjan, bugscorrigidosfev, bugscorrigidosmar, bugscorrigidosabr,
                bugscorrigidosmai, bugscorrigidosjun, bugscorrigidosjul, bugscorrigidosago, bugscorrigidosset, bugscorrigidosout,
            bugscorrigidosnov, bugscorrigidosdez};

            List<LineSeriesData> encontrados = new List<LineSeriesData>();
            List<LineSeriesData> corrigidos = new List<LineSeriesData>();

            encontradoValores.ForEach(p => encontrados.Add(new LineSeriesData { Y = p }));
            corrigidosValores.ForEach(p => corrigidos.Add(new LineSeriesData { Y = p }));

            ViewData["encontrados"] = encontrados;
            ViewData["corrigidos"] = corrigidos;

            var qtdbugsAberto = _context.Bugs.Where(i=>i.EstadosBug.NomeEstado=="Aberto").Count().ToString();
            var qtdbugsEmTrat = _context.Bugs.Where(i => i.EstadosBug.NomeEstado == "Em Tratamento").Count().ToString();
            var qtdbugsCorrigido = _context.Bugs.Where(i => i.EstadosBug.NomeEstado == "Corrigido").Count().ToString();

            var qtdprojsNoPrazo = _context.Projetos.Where(i => i.EstadoProj == "No Prazo").Count().ToString();
            var qtdprojsEmAtraso = _context.Projetos.Where(i => i.EstadoProj == "Em Atraso").Count().ToString();
            var qtdprojsFechado = _context.Projetos.Where(i => i.EstadoProj == "Fechado").Count().ToString();

            var bugtipo500 = _context.Bugs.Where(i => i.TiposBugs.TipoBug == "Erro 500").Count().ToString();
            var bugtipo404 = _context.Bugs.Where(i => i.TiposBugs.TipoBug == "Erro 404").Count().ToString();
            var bugtipoInterface = _context.Bugs.Where(i => i.TiposBugs.TipoBug == "Interface").Count().ToString();
            var bugtipoFluxo = _context.Bugs.Where(i => i.TiposBugs.TipoBug == "Fluxo").Count().ToString();
            var bugtipoCalc =  _context.Bugs.Where(i => i.TiposBugs.TipoBug == "Calculo").Count().ToString();

            var bugtestetipoUni = _context.Tasks.Where(i => i.TiposTestes.TipoTeste == "Unitário").Select(b => b.QtdBugsTsk).FirstOrDefault();
            var bugtestetipoInt = _context.Tasks.Where(i => i.TiposTestes.TipoTeste == "Integração").Select(b => b.QtdBugsTsk).FirstOrDefault();
            var bugtestetipoFum = _context.Tasks.Where(i => i.TiposTestes.TipoTeste == "Fumaça").Select(b => b.QtdBugsTsk).FirstOrDefault();
            var bugtestetipoInf = _context.Tasks.Where(i => i.TiposTestes.TipoTeste == "Interface").Select(b => b.QtdBugsTsk).FirstOrDefault();
            var bugtestetipoReg = _context.Tasks.Where(i => i.TiposTestes.TipoTeste == "Regressão").Select(b => b.QtdBugsTsk).FirstOrDefault();
            var bugtestetipoPer = _context.Tasks.Where(i => i.TiposTestes.TipoTeste == "Performance/Carga").Select(b => b.QtdBugsTsk).FirstOrDefault();
            var bugtestetipoBeta = _context.Tasks.Where(i => i.TiposTestes.TipoTeste == "Beta/Aceitação").Select(b => b.QtdBugsTsk).FirstOrDefault();

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
            ViewData["qtdprojsFechado"] = qtdprojsFechado;

            ViewData["bugtipo500"] = bugtipo500;
            ViewData["bugtipo404"] = bugtipo404;
            ViewData["bugtipoInterface"] = bugtipoInterface;
            ViewData["bugtipoFluxo"] = bugtipoFluxo;
            ViewData["bugtipoCalc"] = bugtipoCalc;

            ViewData["bugtestetipoUni"] = bugtestetipoUni;
            ViewData["bugtestetipoInt"] = bugtestetipoInt;
            ViewData["bugtestetipoFum"] = bugtestetipoFum;
            ViewData["bugtestetipoInf"] = bugtestetipoInf;
            ViewData["bugtestetipoReg"] = bugtestetipoReg;
            ViewData["bugtestetipoPer"] = bugtestetipoPer;
            ViewData["bugtestetipoBeta"] = bugtestetipoBeta;

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

        public ActionResult ListarBugTipoTask(string tipotask)
        {
            var bugtipotask = _context.Bugs.Where(n => n.Tasks.TiposTestes.TipoTeste == tipotask)
                 .Include(t => t.Tasks)
                .Include(p => p.Devs)
                .Include(p => p.Tasks.Projetos)
                .Include(e => e.EstadosBug)
                .Include(t => t.TiposBugs)
                .ToList();

            var tipoTeste = _context.Tasks
               .Where(n => n.TiposTestes.TipoTeste == tipotask)
               .Select(n => n.TiposTestes.TipoTeste)
               .FirstOrDefault();
            ViewData["tipoTeste"] = tipoTeste;

            return View("ListarBugTipoTask", bugtipotask);

        }
    }
}