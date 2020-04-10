using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskMaster.Models;

namespace TaskMaster.Controllers.Api
{
    [Authorize]
    public class TasksController : ApiController
    {
        private ApplicationDbContext _context;

        public TasksController()
        {
            _context = new ApplicationDbContext();
        }
        // GET /api/tasks
        public IEnumerable<Tasks> GetTasks()
        {
            return _context.Tasks.ToList();
        }

        // GET /api/tasks/id
        public Tasks GetTasks(int id)
        {
            var task = _context.Tasks.SingleOrDefault(c => c.TasksId == id);

            if (task == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return task;
        }

        [Authorize(Roles = NomeRoles.tester + "," + NomeRoles.admin)]
        // POST /api/tasks/
        public Tasks PostTasks(Tasks task)
        {
           if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            _context.Tasks.Add(task);
            _context.SaveChanges();

            return task;
        }

        [Authorize(Roles = NomeRoles.tester + "," + NomeRoles.admin)]
        //PUT /api/tasks/id
        [HttpPut]
        public void UpdateTasks(int id, Tasks tasks)
        {
        if (!ModelState.IsValid)
        throw new HttpResponseException(HttpStatusCode.BadRequest);
    
        var taskInDb = _context.Tasks.SingleOrDefault(c => tasks.TasksId == id);
        if (taskInDb==null)
        throw new HttpResponseException(HttpStatusCode.NotFound);

        taskInDb.NomeTask = tasks.NomeTask;
        taskInDb.DataInicio = tasks.DataInicio;
        taskInDb.DataEstimada = tasks.DataEstimada;
        taskInDb.TestersId = tasks.TestersId;
        taskInDb.ProjetosId = tasks.ProjetosId;

        _context.SaveChanges();
        }


        [Authorize(Roles = NomeRoles.tester + "," + NomeRoles.admin)]
        // DELETE /api/tasks/
        [HttpDelete]
        public void DeleteTasks(int id)
        {

            var taskid = _context.Tasks.Where(p => p.TasksId == id).Select(t => t.ProjetosId).SingleOrDefault();
            var projetid = _context.Projetos.Where(t => t.ProjetosId == taskid).Select(p => p.ProjetosId).SingleOrDefault();

            var bugsintask = _context.Bugs.Where(c => c.TasksId == id).ToList();
            if (bugsintask.Count > 0 )
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
            else
            { 
            var taskInDb = _context.Tasks.SingleOrDefault(c => c.TasksId == id);
            if (taskInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Tasks.Remove(taskInDb);
            _context.SaveChanges();

                var sqlQtdTaskPrj = @"Update [Projetos] SET QtdTasksPrj = (QtdTasksPrj-1) WHERE ProjetosId = @ProjetosId";
                _context.Database.ExecuteSqlCommand(
                    sqlQtdTaskPrj,
                    new SqlParameter("@ProjetosId", projetid));

                var sqlRatioBugsPrj = @"Update [Projetos] SET BugsRatio = (QtdBugsPrj/QtdTasksPrj) WHERE ProjetosId = @ProjetosId";
                _context.Database.ExecuteSqlCommand(
                    sqlRatioBugsPrj,
                    new SqlParameter("@ProjetosId", projetid));

            }
        }
    }
}
