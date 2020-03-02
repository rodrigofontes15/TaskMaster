﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskMaster.Models;
using TaskMaster.ViewModels;

namespace TaskMaster.Controllers
{
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
                _context.Tasks.Add(task);
            else
            {
                var taskInDb = _context.Tasks.Single(p => p.TasksId == task.TasksId);
                taskInDb.NomeTask = task.NomeTask;
                taskInDb.DataInicio = task.DataInicio;
                taskInDb.DataEstimada = task.DataEstimada;
                taskInDb.TestersId = task.TestersId;
                taskInDb.ProjetosId = task.ProjetosId;
                taskInDb.TiposTestesId = task.TiposTestesId;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Tasks");
        }


        public ViewResult Index()
        {
            var tasks = _context.Tasks
                .Include(g => g.Projetos)
                .Include(g => g.Testers)
                .Include(t=>t.TiposTestes)
                .ToList();

            return View(tasks);
        }


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
    }
}