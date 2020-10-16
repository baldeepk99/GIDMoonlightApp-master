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
    public class HomeController : Controller
    {
        private readonly MoonLightContext _context;

        public HomeController(MoonLightContext context)
        {
            _context=context;
            List<Businesses> businesses = new List<Businesses>();
            foreach(Businesses b 
                in _context.Businesses)
            {
                businesses.Add(b);
            }
            foreach(Jobs j in _context.Jobs)
            {
                foreach(Businesses b in businesses)
                {
                    if(b.CompanyId == j.CompanyId)
                    {
                        j.Company = b;
                    }
                }
            }
        }

        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            return View();
        }
        public IActionResult BusinessLogin()
        {
            HttpContext.Session.Clear();
            return View();
        }


        public ViewResult Registration()
        {
            return View();
        }
        public ViewResult BusinessRegister()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BusinessLogin(Businesses formResponse)
        {
            Businesses logCheck = null;
            foreach (Businesses c in _context.Businesses)
            {
                if (c.CompanyLogin == formResponse.CompanyLogin)
                {
                    logCheck = _context.Businesses.Find(c.CompanyId);
                }
            }
            if (logCheck == null)
            {
                ViewBag.errorMessage = "Wrong Username/Password";
                return View("Index");
            }
            if (formResponse.Password.Equals(logCheck.Password))
            {
                HttpContext.Session.SetJson("Business", formResponse);
                ViewBag.business = formResponse.CompanyName;
                ViewBag.User = formResponse.CompanyLogin;
                ViewBag.UserType = "business";
                HttpContext.Session.SetString("UserType", "customer");
                return View("BusinessHomePage");
            }
            else
            {
                ViewBag.errorMessage = "Wrong Username/Password";
                return View("BusinessLogin");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Customers formResponse)
        {
            Customers logCheck=null;
            foreach (Customers c in _context.Customers)
            {
                if (c.UserLogin == formResponse.UserLogin)
                {
                    logCheck = _context.Customers.Find(c.CustomerId);
                }
            }
            if(logCheck==null)
            {
                ViewBag.errorMessage = "Wrong Username/Password";
                return View("Index");
            }
            if(formResponse.Password.Equals(logCheck.Password))
            {

                HttpContext.Session.SetJson("Customer", logCheck);
                ViewBag.customer = formResponse.FirstName + " " + formResponse.LastName;
                ViewBag.User = formResponse.UserLogin;
                ViewBag.UserType = "customer";
               
   
               
                HttpContext.Session.SetString("UserType", "customer");
                LoadCustomerViewBags();
                return View("JobSearch");
            }
            else
            {
                ViewBag.errorMessage = "Wrong Username/Password";
                return View("Index");
            }
        }
        public IActionResult JobSearch()
        {

            /* foreach (Jobs j in _context.Jobs)
             {
                 jobtypes.Add(j.JobType);

             }*/
            LoadCustomerViewBags();
            ViewBag.SelectedCategory = "";
            ViewBag.SelectedLocation = "";
            return View();
        }

        public IActionResult CustomerProfile()
        {
            LoadCustomerViewBags();
            Customers customer = ViewBag.LoggedCustomer;
            
            return View(customer);
        }

        [HttpPost]
        public IActionResult CustomerProfile(Customers customer)
        {
            LoadCustomerViewBags();
            Customers obj = _context.Customers.Find(customer.CustomerId);
            obj.FirstName = customer.FirstName;
            obj.LastName = customer.LastName;
            obj.Email = customer.Email;
            obj.ContactNumber = customer.ContactNumber;
            obj.Address = customer.Address;
            obj.City = customer.City;
            obj.PostalCode = customer.PostalCode;
            obj.Province = customer.Province;
            obj.BirthDate = customer.BirthDate;
            obj.Password = customer.Password;
            

            int count = _context.SaveChanges();
            if (count >= 1)
                ViewBag.SuccessMessage = "Profile Updated Successfully!";
            else
                ViewBag.SuccessMessage = "Profile Failed to Update";

            return View(customer);
        }
        public IActionResult ViewCompany(int id)
        {
            ViewBag.User = HttpContext.Session.GetJson<Customers>("Customer").UserLogin;
            List<string> jobtypes = new List<string>();
            Businesses businesses = _context.Businesses.Find(id);

            ViewBag.JobTypes = jobtypes;
            ViewBag.UserType = HttpContext.Session.GetString("UserType");
            return View(businesses);
        }

        public IActionResult MyServices()
        {
            LoadCustomerViewBags();
            Customers customer = ViewBag.LoggedCustomer;
            var services = _context.Services.Where(c => c.CustomerId == customer.CustomerId).Distinct().ToList();
            JobsReviewRepository jobRepo = new JobsReviewRepository();
            jobRepo.CustomerServices = services;

            return View(jobRepo);
        }
        public IActionResult ViewService(int id)
        {
            LoadCustomerViewBags();
            JobsReviewRepository jobRepo = new JobsReviewRepository();
            jobRepo.Jobs = new List<Jobs>();
            Jobs job = _context.Jobs.Find(id);
            jobRepo.Jobs.Add(job);

           
            return View(jobRepo);
        }
        [HttpPost]
        public IActionResult BookService(JobsReviewRepository jobRepo)
        {
            LoadCustomerViewBags();
           
            JobsReviewRepository newJobRepo = new JobsReviewRepository();
            newJobRepo.Jobs = new List<Jobs>();
            Jobs job = _context.Jobs.Find(jobRepo.Service.JobId);
            newJobRepo.Jobs.Add(job);

            Services services = new Services();
            services.Quantity = jobRepo.Service.Quantity;
            services.JobId = jobRepo.Service.JobId;
            services.CustomerId = jobRepo.Service.CustomerId;
            services.Price = (Convert.ToDecimal(jobRepo.Service.Quantity) * Convert.ToDecimal(jobRepo.Jobs[0].ServiceCharge)) + Convert.ToDecimal(jobRepo.Jobs[0].BookingFee);
            services.DateOrder = jobRepo.Service.DateOrder;

            newJobRepo.Service = services;

            _context.Services.Add(services);
            int count = _context.SaveChanges();
            if (count >= 1)
                ViewBag.SuccessMessage = "Service Booking Confirmed!";
            else
                ViewBag.SuccessMessage = "Failed to Book Service";


            return View(newJobRepo);
        }
        [HttpPost]
        public IActionResult JobSearch(string desc, string location)
        {
            ViewBag.SelectedCategory = desc;
            ViewBag.SelectedLocation = location;
            LoadCustomerViewBags();

            
                JobsReviewRepository jobRepo = new JobsReviewRepository();
                jobRepo.Jobs = new List<Jobs>();
                jobRepo.Reviews = new List<Reviews>();
               
                if(!String.IsNullOrEmpty(desc) && String.IsNullOrEmpty(location))
                {
                    jobRepo.Jobs = _context.Jobs.Where(c => c.JobType.ToLower().Contains(desc.ToLower())).Distinct().ToList();
                }else if(!String.IsNullOrEmpty(desc) && !String.IsNullOrEmpty(location))
                {
                    jobRepo.Jobs = _context.Jobs.Where(c => c.JobType.ToLower().Contains(desc.ToLower()) && c.Company.City.ToLower().Contains(location.ToLower())).Distinct().ToList();
                }
                else if (String.IsNullOrEmpty(desc) && !String.IsNullOrEmpty(location))
                {
                    jobRepo.Jobs = _context.Jobs.Where(c => c.Company.City.ToLower().Contains(location.ToLower())).Distinct().ToList();
                }
                else
                {
                    jobRepo.Jobs = _context.Jobs.Distinct().ToList();
                }

                if (jobRepo.Jobs.Count == 0)
                {
                    ViewBag.InfoMessage = "No Services Available";
                }
                else
                {
                    for (int i = 0; i < jobRepo.Jobs.Count(); i++)
                    {
                        jobRepo.Reviews = _context.Reviews.Where(c => c.JobId == jobRepo.Jobs[i].JobId).Distinct().ToList();
                      
                    }
                }
                return View(jobRepo);
            
        }

        public IActionResult CompareJobDetails(int id)
        {
            ViewBag.User = HttpContext.Session.GetJson<Customers>("Customer").UserLogin;

            JobsReviewRepository jobRepo = new JobsReviewRepository();
            jobRepo.Jobs = new List<Jobs>();
            jobRepo.Reviews = new List<Reviews>();

            jobRepo.Jobs.Add(_context.Jobs.Find(id));

            for (int i = 0; i < jobRepo.Jobs.Count(); i++)
            {
                foreach (Reviews r in _context.Reviews)
                {
                    if (r.JobId == jobRepo.Jobs[i].JobId)
                    {
                        jobRepo.Reviews.Add(r);
                    }
                }
            }
            return View(jobRepo);
        }

        [HttpPost]
        public IActionResult Contact(int i)
        {
            Jobs j = new Jobs();
            j = _context.Jobs.Find(i);
            j.Company = _context.Businesses.Find(j.CompanyId);
            ViewBag.User = HttpContext.Session.GetJson<Customers>("Customer").UserLogin;
            return View(j);
        }




        [HttpPost]
        public IActionResult SideToSideComparison(List<int> toCompare)
        {
            ViewBag.User = HttpContext.Session.GetJson<Customers>("Customer").UserLogin;
            List<JobsReviewRepository> jobRepo = new List<JobsReviewRepository>();
            JobsReviewRepository toAdd = new JobsReviewRepository();
            JobsReviewRepository toAdd2 = new JobsReviewRepository();
            if(toCompare.Count()==0)
            {
                return NotFound();
            }
            toAdd = GetReviews(toCompare[0]);
            jobRepo.Add(toAdd);
            if (toCompare.Count()<2)
            {
                return View(jobRepo);
            }
            else
            {
                toAdd2 = GetReviews(toCompare[1]);
                jobRepo.Add(toAdd2);
            }
            return View(jobRepo);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpPost]
        
        public IActionResult Registration(Customers customer)
        {
            var existingUser = _context.Customers.Where(obj => obj.UserLogin == customer.UserLogin).FirstOrDefault();

            if (existingUser == null)
            {
                
                _context.Customers.Add(customer);
                int count = _context.SaveChanges();
                if (count >= 1)
                    ViewBag.SuccessMessage = "Customer Registeration Successfull";
                else
                    ViewBag.SuccessMessage = "Customer Registeration Failed";
            }
            else
            {
                ViewBag.ErrorMessage = "Username already exists! Please try different username";
            }

            return View(customer);
        }

        [HttpPost]

        public IActionResult BusinessRegister(Businesses businesses)
        {
            var existingUser = _context.Businesses.Where(obj => obj.CompanyLogin == businesses.CompanyLogin).FirstOrDefault();

            if(existingUser == null)
            {
                businesses.RegistrationDate = DateTime.Now;
                _context.Businesses.Add(businesses);
                int count = _context.SaveChanges();
                if(count >= 1)
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

        public JobsReviewRepository GetReviews(int id)
        {
            JobsReviewRepository jobRepo = new JobsReviewRepository();
            jobRepo.Jobs = new List<Jobs>();
            jobRepo.Reviews = new List<Reviews>();

            jobRepo.Jobs.Add(_context.Jobs.Find(id));

            for (int i = 0; i < jobRepo.Jobs.Count(); i++)
            {
                foreach (Reviews r in _context.Reviews)
                {
                    if (r.JobId == jobRepo.Jobs[i].JobId)
                    {
                        jobRepo.Reviews.Add(r);
                    }
                }
            }
            return jobRepo;
        }

        public void LoadCustomerViewBags()
        {
            ViewBag.User = HttpContext.Session.GetJson<Customers>("Customer").UserLogin;
            ViewBag.LoggedCustomerId = HttpContext.Session.GetJson<Customers>("Customer").CustomerId;
            ViewBag.LoggedCustomer = HttpContext.Session.GetJson<Customers>("Customer");
            List<string> jobtypes = new List<string>();
            List<string> locations = new List<string>();
            locations = _context.Businesses.Select(c => c.City).Distinct().ToList();
            jobtypes = _context.Jobs.Select(c => c.JobType).Distinct().ToList();

            ViewBag.JobTypes = jobtypes;
            ViewBag.Locations = locations;
            ViewBag.UserType = HttpContext.Session.GetString("UserType");
        }
    }
}
