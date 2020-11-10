using System;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using MoonlightGID.Infrastructure;
using MoonlightGID.Models;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;

namespace MoonlightGID.Controllers
{
    public class BusinessController : Controller
    {
        private readonly MoonLightContext _context;

        public BusinessController(MoonLightContext context)
        {
            _context = context;
            List<Businesses> businesses = new List<Businesses>();
            foreach (Businesses b
                in _context.Businesses)
            {
                businesses.Add(b);
            }
            foreach (Jobs j in _context.Jobs)
            {
                foreach (Businesses b in businesses)
                {
                    if (b.CompanyId == j.CompanyId)
                    {
                        j.Company = b;
                    }
                }
            }
        }

        // GET: BusinessController
        public ActionResult Index()
        {
            HttpContext.Session.Clear();
            return View();
        }
        public ViewResult Registration()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Businesses formResponse)
        {
            Businesses logCheck = _context.Businesses.Where(obj => 
                        obj.CompanyLogin.Equals(formResponse.CompanyLogin) && obj.Password.Equals(formResponse.Password)).FirstOrDefault();
            
            
            if (logCheck != null)
            {
                logCheck.Jobs = null;
                HttpContext.Session.SetJson("Business", logCheck);
                HttpContext.Session.SetString("UserType", "business");
                HttpContext.Session.SetString("UserLogin", logCheck.CompanyLogin);
                HttpContext.Session.SetString("LoggedUserId", logCheck.CompanyId.ToString());
                List<Jobs> myJobs = _context.Jobs.Where(obj => obj.CompanyId == logCheck.CompanyId).ToList();
                logCheck.Jobs = myJobs;
                LoadBusinessViewBags();
                return View("Home",logCheck);
            }
            else
            {
                ViewBag.errorMessage = "Wrong Username/Password";
                return View("Index");
            }
        }
        public ActionResult Home()
        {
            if (HttpContext.Session.GetString("UserLogin") == null)
            {
                return View("Index");
            }
            LoadBusinessViewBags();
            Businesses business = HttpContext.Session.GetJson<Businesses>("Business");
            List<Jobs> myJobs = _context.Jobs.Where(obj => obj.CompanyId == business.CompanyId).ToList();
            business.Jobs = myJobs;
            return View(business);
        }

        public ActionResult Bookings()
        {
            if (HttpContext.Session.GetString("UserLogin") == null)
            {
                return View("Index");
            }
            LoadBusinessViewBags();
            Businesses business = HttpContext.Session.GetJson<Businesses>("Business");
            List<Services> myServices = _context.Services.Where(obj => obj.Job.Company.CompanyId == business.CompanyId).ToList();
            foreach (var item in myServices)
            {
                Customers customers = _context.Customers.Find(item.CustomerId);
                item.Customer = customers;
            }
            return View(myServices);
        }

        [HttpPost]
        public IActionResult Registration(Businesses businesses)
        {
            var existingUser = _context.Businesses.Where(obj => obj.CompanyLogin == businesses.CompanyLogin).FirstOrDefault();

            if (existingUser == null)
            {
                businesses.RegistrationDate = DateTime.Now;
                _context.Businesses.Add(businesses);
                int count = _context.SaveChanges();
                if (count >= 1)
                    ViewBag.SuccessMessage = "Business Registeration Successfull";
                else
                    ViewBag.SuccessMessage = "Business Registeration Failed";
            }
            else
            {
                ViewBag.ErrorMessage = "Username already exists! Please try different username";
            }

            return View(businesses);
        }

        public IActionResult BusinessProfile()
        {
            if (HttpContext.Session.GetString("UserLogin") == null)
            {
                return View("Index");
            }
            LoadBusinessViewBags();
            Businesses business = HttpContext.Session.GetJson<Businesses>("Business");
            business.Jobs = null;
            return View(business);
        }

        [HttpPost]
        public IActionResult BusinessProfile(Businesses business)
        {
            if (HttpContext.Session.GetString("UserLogin") == null)
            {
                return View("Index");
            }
            LoadBusinessViewBags();
            Businesses obj = _context.Businesses.Find(business.CompanyId);
            obj.CompanyLogin = business.CompanyLogin;
            obj.Password = business.Password;
            obj.CompanyName = business.CompanyName;
            obj.ContactNumber = business.ContactNumber;
            obj.EmailAddress = business.EmailAddress;
            obj.FullName = business.FullName;
            obj.Address = business.Address;
            obj.City = business.City;
            obj.PostalCode = business.PostalCode;
            obj.Province = business.Province;
            obj.RegistrationDate = business.RegistrationDate;
            obj.OfficeHours = business.OfficeHours;
            obj.WorkingDays = business.WorkingDays;


            int count = _context.SaveChanges();
            if (count >= 1)
                ViewBag.SuccessMessage = "Profile Updated Successfully!";
            

            return View(business);
        }


        public void LoadBusinessViewBags()
        {

            int CompanyId = HttpContext.Session.GetJson<Businesses>("Business").CompanyId;
           // ViewBag.User = HttpContext.Session.GetJson<Businesses>("Business").CompanyLogin;
          //  ViewBag.LoggedUserId = HttpContext.Session.GetJson<Businesses>("Business").CompanyId;
          //  ViewBag.LoggedUser = HttpContext.Session.GetJson<Businesses>("Business");
            List<string> jobtypes = new List<string>();
            List<string> locations = new List<string>();
            locations = _context.Businesses.Select(c => c.City).Distinct().ToList();
            jobtypes = _context.Jobs.Select(c => c.JobType).Distinct().ToList();
            List<Jobs> myJobs = _context.Jobs.Where(obj => obj.CompanyId == CompanyId).ToList();
            JobsReviewRepository repository = new JobsReviewRepository();
            repository.Jobs = myJobs;
           
            ViewBag.JobTypes = jobtypes;
            ViewBag.Locations = locations;
            ViewBag.Repository = repository;
            ViewBag.UserType = HttpContext.Session.GetString("UserType");
        }

        public ActionResult AddService()
        {
            if (HttpContext.Session.GetString("UserLogin") == null)
            {
                return View("Index");
            }
            LoadBusinessViewBags();
            Businesses business = HttpContext.Session.GetJson<Businesses>("Business");
            Jobs obj = new Jobs();
            obj.Company = business;
            obj.CompanyId = business.CompanyId;
            return View(obj);
        }

        // POST: BusinessController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddService(Jobs job)
        {
            if (HttpContext.Session.GetString("UserLogin") == null)
            {
                return View("Index");
            }
            LoadBusinessViewBags();
            try
            {
                Businesses business = HttpContext.Session.GetJson<Businesses>("Business");
                job.Company = null;
                job.CompanyId = business.CompanyId;
                _context.Jobs.Add(job);
                int count = _context.SaveChanges();
                if (count >= 1)
                    ViewBag.SuccessMessage = "New Job added Successfull";
                else
                    ViewBag.SuccessMessage = "Job addition Failed";

                return RedirectToAction(nameof(Home));
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = "Exception: " + ex.Message.ToString();
                Businesses business = HttpContext.Session.GetJson<Businesses>("Business");

                job.CompanyId = business.CompanyId;
                return View(job);
            }
        }
        public ActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("UserLogin") == null)
            {
                return View("Index");
            }
            LoadBusinessViewBags();
            Jobs obj = _context.Jobs.Find(id);
            return View(obj);
        }

        
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Jobs job)
        {
            if (HttpContext.Session.GetString("UserLogin") == null)
            {
                return View("Index");
            }
            LoadBusinessViewBags();
            try
            {
                
                Jobs obj = _context.Jobs.Find(job.JobId);
                obj.JobName = job.JobName;
                obj.JobType = job.JobType;
                obj.Description = job.Description;
                obj.BookingFee = job.BookingFee;
                obj.ServiceCharge = job.ServiceCharge;

                 _context.SaveChanges();
                

                ViewBag.SuccessMessage = "Service Updated Successfully!";
                Businesses business = HttpContext.Session.GetJson<Businesses>("Business");
                List<Jobs> myJobs = _context.Jobs.Where(obj => obj.CompanyId == business.CompanyId).ToList();
                business.Jobs = myJobs;
                return RedirectToAction(nameof(Home));
                //return View("Home", business);
            }
            catch(Exception ex)
            {
                ViewBag.ErroMessage = "Exception: " + ex.Message.ToString();
                Jobs obj = _context.Jobs.Find(job.JobId);
                job.Company = obj.Company;
                return View(job);
            }
        }

        // GET: BusinessController/Delete/5
        public ActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("UserLogin") == null)
            {
                return View("Index");
            }
            LoadBusinessViewBags();
            Jobs job = _context.Jobs.Find(id);
            _context.Jobs.Remove(job);
            int count = _context.SaveChanges();

            if (count >= 1)
                ViewBag.SuccessMessage = "Service Deleted Successfully!";
            else
                ViewBag.ErrorMessage = "Service Failed to Delete";

           
            return RedirectToAction(nameof(Home));
        }

        
    }
}
