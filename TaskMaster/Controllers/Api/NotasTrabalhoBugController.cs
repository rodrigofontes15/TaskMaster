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
        // GET /api/bugs
        public IEnumerable<NotasTrabalhoBug> GetNotasTrabalhoBug()
        {
            return _context.NotasTrabalhoBug.ToList();
        }
    }
}
