using System;
using System.Collections.Generic;
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
            if (bugInDb==null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            bugInDb.DescBug = bugs.DescBug;
            bugInDb.DataBug = bugs.DataBug;
            bugInDb.DataEstimada = bugs.DataBug;
            bugInDb.TasksId = bugs.TasksId;
            bugInDb.UrlRepoCodigo = bugs.UrlRepoCodigo;

            _context.SaveChanges();
        }

        [Authorize(Roles = NomeRoles.tester + "," + NomeRoles.admin)]
        // DELETE /api/bugs/
        [HttpDelete]
        public void DeleteBug(int id)
        {
            
            var bugInDb = _context.Bugs.SingleOrDefault(c => c.BugsId == id);
            if (bugInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Bugs.Remove(bugInDb);
            _context.SaveChanges();
        }

    }
}
