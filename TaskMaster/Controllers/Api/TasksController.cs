using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskMaster.Models;

namespace TaskMaster.Controllers.Api
{
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
        public Tasks GetTask(int id)
        {
            var task = _context.Tasks.SingleOrDefault(c => c.TasksId == id);

            if (task == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return task;
        }

        // POST /api/tasks/
        public Tasks PostTask(Tasks task)
        {
           if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            _context.Tasks.Add(task);
            _context.SaveChanges();

            return task;
        }

        // PUT /api/tasks/id
        [HttpPut]
        public void UpdateTask(int id, Tasks tasks)
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

        // DELETE /api/tasks/
        [HttpDelete]
        public void DeleteTask(int id)
        {
            
            var taskInDb = _context.Tasks.SingleOrDefault(c => c.TasksId == id);
            if (taskInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Tasks.Remove(taskInDb);
            _context.SaveChanges();
        }

    }
}
