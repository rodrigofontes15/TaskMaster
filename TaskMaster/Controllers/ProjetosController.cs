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

        public ViewResult NovoProjeto()
        {
            var gerenteProjetos = _context.GerenteProjs.ToList();
            var viewModel = new NovoProjetoViewModel
            {
                GerenteProjs = gerenteProjetos
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CriarProjeto(Projetos projetos)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new NovoProjetoViewModel
                {
                    Projetos = projetos,
                    GerenteProjs = _context.GerenteProjs.ToList()
                };
                return View("NovoProjeto", viewModel);
            }
            if (projetos.Id == 0)
                _context.Projetos.Add(projetos);

            _context.SaveChanges();

            return RedirectToAction("Index", "Projetos");
        }


        public ViewResult Index()
        {
            var projetos = _context.Projetos.Include(g => g.GerenteProjs).ToList();

            return View(projetos);
        }


        public ActionResult Detalhes(int id)
        {
            var projeto = _context.Projetos.SingleOrDefault(c => c.Id == id);

            if (projeto == null)
                return HttpNotFound();

            return View(projeto);
        }
    }

}