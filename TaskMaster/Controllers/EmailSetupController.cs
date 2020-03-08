﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using TaskMaster.Models;

namespace TaskMaster.Controllers
{

    [AllowAnonymous]
    public class EmailSetupController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(TaskMaster.Models.gmail model)
        {            
            MailMessage mm = new MailMessage("taskmasterbugs@gmail.com", "taskmasterbugs@gmail.com");           
            mm.Subject = model.Subject;
            mm.Body = model.Body;
            mm.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            NetworkCredential nc = new NetworkCredential("taskmasterbugs@gmail.com", "sts25wka");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = nc;
            smtp.Send(mm);
            ViewBag.Message = "Solicitação de Usuário Enviada com Sucesso ao ADM!";
            return RedirectToAction("Index", "Home");
        }
    }
}
