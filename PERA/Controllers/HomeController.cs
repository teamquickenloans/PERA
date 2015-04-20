using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Postal;
using System.Net.Mail;

namespace PERA.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            /*var smtpClient = new SmtpClient();
            var msg = new MailMessage();
            msg.To.Add("soullie7@msu.edu");
            msg.Subject = "Test";
            msg.Body = "This is just a test email";
            smtpClient.Send(msg);*/
            return View();
        }

        [HttpPost]
        public ContentResult Upload(HttpPostedFileBase file)
        {
            var filename = Path.GetFileName(file.FileName);
            var path = Path.Combine(Server.MapPath("~/uploads"), filename);
            file.SaveAs(path);

            return new ContentResult
            {
                ContentType = "text/plain",
                Content = filename,
                ContentEncoding = Encoding.UTF8
            };
        }

    }
}