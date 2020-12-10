using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoonlightGID.Models
{
    public partial class Customers
    {
        public Customers()
        {
            Services = new HashSet<Services>();
        }

        public int CustomerId { get; set; }
        public string UserLogin { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

        // Custom error message if the province wasn't selected.
        [Required(ErrorMessage ="Please select a province.")]
        public string Province { get; set; }

        // Validation for Postal Code | Only Uppercase and space in between the 3 pairs.
        [RegularExpression("^[A-Z][0-9][A-Z][ ][0-9][A-Z][0-9]", ErrorMessage ="Correct Format : A1B 2C3, UpperCase and space in between.")]
        public string PostalCode { get; set; }

        // Validation for phone number which needs '-'.
        [RegularExpression("^(1-)?\\d{3}-\\d{3}-\\d{4}$", ErrorMessage ="Please enter in this format : 123-456-7890")]
        public string ContactNumber { get; set; }

        // Validation for email
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid \n eg:(blakesheldon@gmail.com)")]
        public string Email { get; set; }

        public virtual ICollection<Services> Services { get; set; }
    }
}
