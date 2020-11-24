﻿using System;
using System.Collections.Generic;

namespace MoonlightGID.Models
{
    public partial class Businesses
    {
        public Businesses()
        {
            Jobs = new HashSet<Jobs>();
            Reviews = new HashSet<Reviews>();
        }

        public int CompanyId { get; set; }
        public string CompanyLogin { get; set; }
        public string Password { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string ContactNumber { get; set; }
        public string EmailAddress { get; set; }
        public string FullName { get; set; }

        public string OfficeHours { get; set; }
        public string WorkingDays { get; set; }
        
        public DateTime RegistrationDate { get; set; }

        public string ChatLink { get; set; }

        public string CalendlyUrl { get; set; }
        public virtual ICollection<Jobs> Jobs { get; set; }
        public virtual ICollection<Reviews> Reviews { get; set; }
    }
}
