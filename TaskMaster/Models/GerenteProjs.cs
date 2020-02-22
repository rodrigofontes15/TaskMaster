﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskMaster.Models
{
    public class GerenteProjs
    {
        public int GerenteProjsId { get; set; }

        [Required]
        [StringLength(255)]
        public string Nome { get; set; }

        public ICollection<Projetos> Projetos { get; set; }
    }
}