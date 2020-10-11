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

        public ViewResult Home()
        {
            return View();
        }
        public ViewResult JobSearch()
        {
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
                HttpContext.Session.SetJson("Customer", formResponse);
                ViewBag.customer = formResponse.FirstName + " " + formResponse.LastName;
                ViewBag.User = formResponse.UserLogin;
                return View("JobSearch");
            }
            else
            {
                ViewBag.errorMessage = "Wrong Username/Password";
                return View("Index");
            }
        }

        [HttpPost]
        public IActionResult SearchResults(string desc)
        {

            ViewBag.User = HttpContext.Session.GetJson<Customers>("Customer").UserLogin;
            if (desc==null)
            {
                return View();
            }
            JobsReviewRepository jobRepo = new JobsReviewRepository();
            jobRepo.Jobs = new List<Jobs>();
            jobRepo.Reviews = new List<Reviews>();
            foreach (Jobs j in _context.Jobs)
            {
                if (j.JobType.Contains(desc))
                {
                    jobRepo.Jobs.Add(j);
                }
            }
            if (jobRepo.Jobs.Count == 0)
            {
                ViewBag.errorMessage = "No jobs Found";
            }
            else
            {
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


            /* string constr = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;
           using (SqlConnection con = new SqlConnection(constr)) */
            var conn = "Server=LAPTOP-N0HTAK58\\SQLEXPRESS;Database=MoonLight;Trusted_Connection=True;";
            using (SqlConnection con = new SqlConnection(conn))
            {
               string query = "INSERT INTO Customers(UserLogin, Password, FirstName, LastName, CityAddress, ZipCode, ContactNumber, BirthDate, Email) VALUES(@UserLogin, @Password, @FirstName, @LastName, @CityAddress, @ZipCode, @ContactNumber, @BirthDate, @Email)";
               query += " SELECT SCOPE_IDENTITY()";
               using (SqlCommand cmd = new SqlCommand(query))
               { 
           

            cmd.Connection = con;
                con.Open();
                    cmd.Parameters.AddWithValue("@UserLogin", customer.UserLogin);
                    cmd.Parameters.AddWithValue("@Password", customer.Password);
                    cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                    cmd.Parameters.AddWithValue("@CityAddress", customer.CityAddress);
                    cmd.Parameters.AddWithValue("@ZipCode", customer.ZipCode);
                    cmd.Parameters.AddWithValue("@ContactNumber", customer.ContactNumber);
                    cmd.Parameters.AddWithValue("@BirthDate", customer.BirthDate);
                    cmd.Parameters.AddWithValue("@Email", customer.Email);
                   
                    //customer.CustomerId = Convert.ToInt32(cmd.ExecuteScalar());
                    int rowsInserted = cmd.ExecuteNonQuery();

                    if (rowsInserted == 1)
                        ViewBag.errorMessage = " jobs Found";
                    else
                        ViewBag.errorMessage = "No jobs Found";

                    con.Close();

            }
        }
 
        return View(customer);
    }

        [HttpPost]

        public IActionResult BusinessRegister(Businesses businesses)
        {


            /* string constr = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;
           using (SqlConnection con = new SqlConnection(constr)) */
            var conn = "Server=LAPTOP-N0HTAK58\\SQLEXPRESS;Database=MoonLight;Trusted_Connection=True;";
            using (SqlConnection con = new SqlConnection(conn))
            {
                string query = "INSERT INTO Businesses(CompanyLogin, Password, CompanyName, CompanyAddress, ContactNumber, EmailAddress, RegistrationDate) VALUES(@CompanyLogin, @Password, @CompanyName, @CompanyAddress, @ContactNumber, @EmailAddress, @RegistrationDate)";
                query += " SELECT SCOPE_IDENTITY()";
                using (SqlCommand cmd = new SqlCommand(query))
                {


                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@CompanyLogin", businesses.CompanyLogin);
                    cmd.Parameters.AddWithValue("@Password", businesses.Password);
                    cmd.Parameters.AddWithValue("@CompanyName", businesses.CompanyName);
                    cmd.Parameters.AddWithValue("@CompanyAddress", businesses.CompanyAddress);
                    cmd.Parameters.AddWithValue("@ContactNumber", businesses.ContactNumber);
                    cmd.Parameters.AddWithValue("@EmailAddress", businesses.EmailAddress);
                    cmd.Parameters.AddWithValue("@RegistrationDate", businesses.RegistrationDate);

                    //customer.CustomerId = Convert.ToInt32(cmd.ExecuteScalar());
                    int rowsInserted = cmd.ExecuteNonQuery();

                    if (rowsInserted == 1)
                        ViewBag.errorMessage = " jobs Found";
                    else
                        ViewBag.errorMessage = "No jobs Found";

                    con.Close();

                }
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
    }
}
