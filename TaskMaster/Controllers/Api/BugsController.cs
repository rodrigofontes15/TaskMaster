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
    public class BugsController : ApiController
    {
        private ApplicationDbContext _context;

        public BugsController()
        {
            _context = new ApplicationDbContext();
        }
        // GET /api/bugs
        public IEnumerable<Bugs> GetBugs()
        {
            return _context.Bugs.ToList();
        }

        // GET /api/bugs/id
        public Bugs GetBugs(int id)
        {
            var bug = _context.Bugs.SingleOrDefault(c => c.BugsId == id);

            if (bug == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return bug;
        }

        [Authorize(Roles = NomeRoles.tester + "," + NomeRoles.admin)]
        // POST /api/bugs/
        public Bugs PostBugs(Bugs bug)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            _context.Bugs.Add(bug);
            _context.SaveChanges();

            return bug;
        }

        [Authorize(Roles = NomeRoles.tester + "," + NomeRoles.admin + "," + NomeRoles.dev)]
        // PUT /api/bugs/id
        [HttpPut]
        public void UpdateBugs(int id, Bugs bugs)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var bugInDb = _context.Bugs.SingleOrDefault(c => bugs.BugsId == id);
            if (bugInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            bugInDb.DescBug = bugs.DescBug;
            bugInDb.DataBug = bugs.DataBug;
            bugInDb.DataEstimadaBug = bugs.DataEstimadaBug;
            bugInDb.TasksId = bugs.TasksId;
            bugInDb.UrlRepoCodigo = bugs.UrlRepoCodigo;

            _context.SaveChanges();
        }

        [Authorize(Roles = NomeRoles.tester + "," + NomeRoles.admin)]
        // DELETE /api/bugs/
        [HttpDelete]
        public void DeleteBug(int id)
        {

            var taskdobug = _context.Bugs.Where(p => p.BugsId == id).Select(t => t.TasksId).SingleOrDefault();
            var projetidnatask = _context.Tasks.Where(t => t.TasksId == taskdobug).Select(p => p.ProjetosId).SingleOrDefault();
            var dataInDbProjetos = _context.Projetos.Where(p => p.ProjetosId == projetidnatask).Select(d => d.DataReal).SingleOrDefault();
            var databugInDb = _context.Bugs.Where(c => c.BugsId == id).Select(d => d.DataEstimadaBug).SingleOrDefault();
            var bugInDb = _context.Bugs.SingleOrDefault(c => c.BugsId == id);

            var DataNull = "";

            if (bugInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            else
            {
                if (databugInDb == dataInDbProjetos)
                {
                    var sqlDataRealTask = @"Update [Projetos] SET DataReal = @DataReal WHERE ProjetosId = @ProjetosId";

                    _context.Database.ExecuteSqlCommand(
                        sqlDataRealTask,
                        new SqlParameter("@DataReal", DataNull),
                        new SqlParameter("@ProjetosId", projetidnatask));

                    var sqlQtdBugsTask = @"Update [Tasks] SET QtdBugsTsk = (QtdBugsTsk-1) WHERE TasksId = @TasksId";
                    _context.Database.ExecuteSqlCommand(
                        sqlQtdBugsTask,
                        new SqlParameter("@TasksId", taskdobug));

                    var sqlQtdBugsPrj = @"Update [Projetos] SET QtdBugsPrj = (QtdBugsPrj-1) WHERE ProjetosId = @ProjetosId";
                    _context.Database.ExecuteSqlCommand(
                        sqlQtdBugsPrj,
                        new SqlParameter("@ProjetosId", projetidnatask));

                    var sqlRatioBugsPrj = @"Update [Projetos] SET BugsRatio = (QtdBugsPrj/QtdTasksPrj) WHERE ProjetosId = @ProjetosId";
                    _context.Database.ExecuteSqlCommand(
                        sqlRatioBugsPrj,
                        new SqlParameter("@ProjetosId", projetidnatask));

                    _context.Bugs.Remove(bugInDb);
                    _context.SaveChanges();
                }
                else
                {
                    var sqlQtdBugsTask = @"Update [Tasks] SET QtdBugsTsk = (QtdBugsTsk-1) WHERE TasksId = @TasksId";
                    _context.Database.ExecuteSqlCommand(
                        sqlQtdBugsTask,
                        new SqlParameter("@TasksId", taskdobug));

                    var sqlQtdBugsPrj = @"Update [Projetos] SET QtdBugsPrj = (QtdBugsPrj-1) WHERE ProjetosId = @ProjetosId";
                    _context.Database.ExecuteSqlCommand(
                        sqlQtdBugsPrj,
                        new SqlParameter("@ProjetosId", projetidnatask));

                    var sqlRatioBugsPrj = @"Update [Projetos] SET BugsRatio = (QtdBugsPrj/QtdTasksPrj) WHERE ProjetosId = @ProjetosId";
                    _context.Database.ExecuteSqlCommand(
                        sqlRatioBugsPrj,
                        new SqlParameter("@ProjetosId", projetidnatask));

                    _context.Bugs.Remove(bugInDb);
                    _context.SaveChanges();
                }
            }

        }
    }
}
