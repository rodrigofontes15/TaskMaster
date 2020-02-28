using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Drawing.Imaging;

namespace TaskMaster.Models
{
    public class Devs
    {
        public int DevsId { get; set; }

        [Required]
        [StringLength(255)]
        public string DevNome { get; set; }

        public string EmailDev { get; set; }

        public string TelDev { get; set; }

        public string UrlPhotoDev { get; set; }

        public ICollection<Bugs> Bugs { get; set; }
    }
}