using ConnectEvent.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConnectEvent.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            Attendee attendee = new Attendee();
            attendee.Attending = true;
            return View(attendee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "ID,Email,FirstName,LastName,Company,Platform,Attending")] Attendee attendee)
        {
            attendee.ResponseDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Attendees.Add(attendee);
                db.SaveChanges();
                return RedirectToAction("Confirmation");
            }

            return View(attendee);
        }

        [AllowAnonymous]
        public JsonResult ConfirmAttendance(string Phone, string FirstName,string LastName,string Company,string Platform, bool Attending)
        {
            try
            {
                string missingFields = string.Empty;
                if (FirstName == null || FirstName == string.Empty)
                {
                    missingFields = "First Name";
                }
                if (LastName == null || LastName == string.Empty)
                {
                    if (missingFields == string.Empty)
                    {
                        missingFields = "Last Name";
                    }
                    else
                    {
                        missingFields += ", Last Name";
                    }
                }

                if (Company == null || Company == string.Empty)
                {
                    if (missingFields == string.Empty)
                    {
                        missingFields = "Company";
                    }
                    else
                    {
                        missingFields += ", Company";
                    }
                }

                if (Phone == null || Phone == string.Empty)
                {
                    if (missingFields == string.Empty)
                    {
                        missingFields = "Phone";
                    }
                    else
                    {
                        missingFields += ", Phone";
                    }
                }

                if (missingFields != string.Empty)
                {
                    return Json(new { result = "The following fields are required: " + missingFields });
                }
                else
                {
                    Attendee attendee = new Attendee();
                    attendee.ResponseDate = DateTime.Now;
                    attendee.FirstName = FirstName;
                    attendee.LastName = LastName;
                    attendee.Company = Company;
                    attendee.Email = Phone;
                    attendee.Attending = Attending;
                    attendee.Platform = Platform;

                    db.Attendees.Add(attendee);

                    db.SaveChanges();
                       
                }
            }
            catch (Exception ex)
            {
                return Json(new { result = "false" });
            }
            return Json(new { result = "true" });
        }

        public ActionResult Confirmation()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public string Booth1Content()
        {
            //var contentPath = ConfigurationManager.AppSettings["ContentPath"].ToString();
            //StreamReader streamReader = new StreamReader(contentPath + "Booth1Content.html");
            //string text = streamReader.ReadToEnd();
            //streamReader.Close();
            //return Json(new { result = text }, JsonRequestBehavior.AllowGet);

            var hostPath = ConfigurationManager.AppSettings["ContentPath"].ToString();
            return hostPath + "Booth1Content.html";
        }

        [AllowAnonymous]
        [HttpGet]
        public string Booth2Content()
        {
            var hostPath = ConfigurationManager.AppSettings["ContentPath"].ToString();
            //StreamReader streamReader = new StreamReader(contentPath + "Booth2Content.html");
            //string text = streamReader.ReadToEnd();
            //streamReader.Close();
            return hostPath + "Booth2Content.html";
        }

        [AllowAnonymous]
        [HttpGet]
        public string Booth3Content()
        {
            var hostPath = ConfigurationManager.AppSettings["ContentPath"].ToString();
            return hostPath + "Booth3Content.html";
        }

        [AllowAnonymous]
        [HttpGet]
        public string Booth4Content()
        {
            var hostPath = ConfigurationManager.AppSettings["ContentPath"].ToString();
            return hostPath + "Booth4Content.html";
        }

        public ActionResult SendTestMessage()
        {
            return View();
        }

        public ActionResult Welcome()
        {
            return View();
        }

        [HttpPost]
        public void SendMessage(string message)
        {
            string[] parts = message.Split(',');
            if(parts.Count() != null && parts.Count() == 5)
            {
                //lookup based on phone number
                string phoneNumber = parts[4];

                Attendee att = db.Attendees.Where(at => at.Email.Trim() == phoneNumber.Trim()).FirstOrDefault();
                if ((att != null)&&!att.PresentAtEvent)
                {
                    att.PresentAtEvent = true;
                    db.SaveChanges();

                    ChatHub.SendMessage(message);
                }
                //else already here, do nothing
            }
            //else junk data, do nothing
            
        }
    }
}