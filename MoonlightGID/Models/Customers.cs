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
        [Required(ErrorMessage ="Please select a province.")]
        public string Province { get; set; }
        [RegularExpression("^[A-Z][0-9][A-Z][ ][0-9][A-Z][0-9]", ErrorMessage ="Correct Format : A1B 2C3, UpperCase and space in between.")]
        public string PostalCode { get; set; }
        [RegularExpression("^(1-)?\\d{3}-\\d{3}-\\d{4}$", ErrorMessage ="Please enter in this format : 123-456-7890")]
        public string ContactNumber { get; set; }
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid \n eg:(blakesheldon@gmail.com)")]
        public string Email { get; set; }

        public virtual ICollection<Services> Services { get; set; }
    }
}
