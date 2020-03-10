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
    public class ProjetosController : Controller
    {
        private ApplicationDbContext _context;

        public ProjetosController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ViewResult Index()
        {         
            var projetos = _context.Projetos.Include(g => g.GerenteProjs).ToList();

            if (User.IsInRole("gp"))
            {
                return View("Index", projetos);
            }
            else
            {
                if (User.IsInRole("admin"))
                {
                    return View("Index", projetos);
                }
                else
                    return View("Index_SomenteLeitura", projetos);
            }
        }

        [Authorize(Roles = NomeRoles.gp + "," + NomeRoles.admin)]
        public ViewResult NovoProjeto()
        {
            var gerenteProjetos = _context.GerenteProjs.ToList();
            var viewModel = new ProjetoViewModel
            {
                GerenteProjs = gerenteProjetos
            };

            return View("FormProjeto", viewModel);
        }

        [Authorize(Roles = NomeRoles.gp + "," + NomeRoles.admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Salvar(Projetos projetos)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new ProjetoViewModel(projetos)
                {
                    GerenteProjs = _context.GerenteProjs.ToList()
                };

                return View("FormProjeto", viewModel);
            }

            if (projetos.ProjetosId == 0)
                _context.Projetos.Add(projetos);
            else
            {
                var projetoInDb = _context.Projetos.Single(p => p.ProjetosId == projetos.ProjetosId);
                projetoInDb.NomeProjeto = projetos.NomeProjeto;
                projetoInDb.GerenteProjsId = projetos.GerenteProjsId;
                projetoInDb.DataInicio = projetos.DataInicio;
                projetoInDb.DataEstimada = projetos.DataEstimada;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Projetos");
        }

        [Authorize(Roles = NomeRoles.gp + "," + NomeRoles.admin)]
        public ActionResult Editar(int id)
        {
            var projeto = _context.Projetos.SingleOrDefault(c => c.ProjetosId == id);

            if (projeto == null)
                return HttpNotFound();

            var viewModel = new ProjetoViewModel(projeto)
            {
                GerenteProjs = _context.GerenteProjs.ToList()
            };

            return View("FormProjeto", viewModel);
        }

        public ActionResult Detalhes(int id)
        {
            var projeto = _context.Projetos.Where(n => n.ProjetosId == id)
                .Include(g => g.GerenteProjs).ToList();

            return View("Detalhes", projeto);
        }
    }

}