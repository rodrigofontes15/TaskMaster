using System;
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
            MailMessage mm = new MailMessage();
            mm.From = new MailAddress(model.From);
            mm.To.Add("taskmasterbugs@gmail.com");
            mm.Subject = model.Subject;
            mm.Body = model.Body;
            mm.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("relay-hosting.secureserver.net", 25);
            smtp.Credentials = new NetworkCredential("taskmasterbugs@gmail.com", "sts25wka");
            smtp.Port = 25;
            smtp.EnableSsl = false;
            smtp.Send(mm);
            ViewBag.Message = "Solicitação de Usuário Enviada com Sucesso ao ADM!";
            smtp.Dispose();

            return RedirectToAction("Index", "Home");
        }
    }
}
