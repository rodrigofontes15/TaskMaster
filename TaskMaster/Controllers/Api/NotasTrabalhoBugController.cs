using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskMaster.Models;

namespace TaskMaster.Controllers.Api
{
    public class NotasTrabalhoBugController : ApiController
    {
        private ApplicationDbContext _context;

        public NotasTrabalhoBugController()
        {
            _context = new ApplicationDbContext();
        }
        // GET /api/NotasTrabalhoBug/
        public IEnumerable<NotasTrabalhoBug> GetNotasTrabalhoBug()
        {
            return _context.NotasTrabalhoBug.ToList();
        }


        // DELETE /api/NotasTrabalhoBug/
        [HttpDelete]
        public void DeleteNotasTrabalhoBug(int id)
        {

            var notaInDb = _context.NotasTrabalhoBug.SingleOrDefault(c => c.NotasTrabalhoBugId == id);
            if (notaInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.NotasTrabalhoBug.Remove(notaInDb);
            _context.SaveChanges();
        }
    }
}
