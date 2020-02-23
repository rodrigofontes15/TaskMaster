using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskMaster.Models;

namespace TaskMaster.Controllers.Api
{
    public class ProjetosController : ApiController
    {
        private ApplicationDbContext _context;

        public ProjetosController()
        {
            _context = new ApplicationDbContext();
        }
        // GET /api/projetos
        public IEnumerable<Projetos> GetProjetos()
        {
            return _context.Projetos.ToList();
        }

        // GET /api/projetos/id
        public Projetos GetProjeto(int id)
        {
            var projeto = _context.Projetos.SingleOrDefault(c => c.Id == id);

            if (projeto == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return projeto;
        }

        // POST /api/projetos/
        public Projetos PostProjeto(Projetos projeto)
        {
           if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            _context.Projetos.Add(projeto);
            _context.SaveChanges();

            return projeto;
        }

        // PUT /api/projetos/id
        [HttpPut]
        public void UpdateProjeto(int id, Projetos projetos)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var projetoInDb = _context.Projetos.SingleOrDefault(c => projetos.Id == id);
            if (projetoInDb==null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            projetoInDb.NomeProjeto = projetos.NomeProjeto;
            projetoInDb.GerenteProjsId = projetos.GerenteProjsId;
            projetoInDb.DataInicio = projetos.DataInicio;
            projetoInDb.DataEstimada = projetos.DataEstimada;

            _context.SaveChanges();
        }

        // PUT /api/projetos/id
        [HttpDelete]
        public void DeleteProjeto(int id)
        {
            
            var projetoInDb = _context.Projetos.SingleOrDefault(c => c.Id == id);
            if (projetoInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Projetos.Remove(projetoInDb);
            _context.SaveChanges();
        }

    }
}
