using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "Please select a province.")]
        public string Province { get; set; }
        [RegularExpression("^[A-Z][0-9][A-Z][ ][0-9][A-Z][0-9]", ErrorMessage = "Correct Format : A1B 2C3, UpperCase and space in between.")]
        public string PostalCode { get; set; }
        [RegularExpression("^(1-)?\\d{3}-\\d{3}-\\d{4}$", ErrorMessage = "Please enter in this format : 123-456-7890")]
        public string ContactNumber { get; set; }
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid \n eg:(blakesheldon@gmail.com)")]
        public string EmailAddress { get; set; }
        public string FullName { get; set; }

        public string OfficeHours { get; set; }
        public string WorkingDays { get; set; }
        
        public DateTime RegistrationDate { get; set; }

        public string ChatLink { get; set; }

        public virtual ICollection<Jobs> Jobs { get; set; }
        public virtual ICollection<Reviews> Reviews { get; set; }
    }
}
