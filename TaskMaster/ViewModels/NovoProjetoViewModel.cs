using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskMaster.Models;

namespace TaskMaster.ViewModels
{
    public class NovoProjetoViewModel
    {
        public IEnumerable<GerenteProjs> GerenteProjs { get; set; }
        public Projetos Projetos { get; set; }


    }
}