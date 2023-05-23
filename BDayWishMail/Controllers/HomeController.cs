using BDayWishMail.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BDayWishMail.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Send(int a)
        {
            ViewBag.Title = "Home Page";

            return Json(new { message = "Mail Send" });
        }


      [HttpPost]
        public ActionResult Send()
        {
            ClsMail mail = new ClsMail();
            string from = System.Configuration.ConfigurationManager.AppSettings["From"].ToString();
            string Cc = System.Configuration.ConfigurationManager.AppSettings["CC"].ToString();

            string msg = mail.Generate_HTML("");
            bool result =mail.IsSMTP("c-sailee.malvankar@timesgroup.com", "c-sailee.malvankar@timesgroup.com", "c-sailee.malvankar@timesgroup.com", "sailee", "TESTING Birthday Wishes!!");
            //mail.sendMailTo();
            return Json(new { message = "Mail Send" });
        }
    }
}
