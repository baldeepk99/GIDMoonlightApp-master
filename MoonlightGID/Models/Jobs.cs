using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoonlightGID.Models
{
    public partial class Jobs
    {
        public Jobs()
        {
            Reviews = new HashSet<Reviews>();
        }

        public int JobId { get; set; }
       
        public string JobName { get; set; }
        public string JobType { get; set; }
        public string Description { get; set; }
        public int CompanyId { get; set; }
        // Input validation for Money(Booking Fee)
        [RegularExpression("\\d{0,8}(\\.\\d{0,2})?$", ErrorMessage = "Correct format : 34 or 34.44")]
        public string BookingFee { get; set; }
        // Input validation for Money(Service Charge)
        [RegularExpression("\\d{0,8}(\\.\\d{0,2})?$", ErrorMessage = "Correct format : 34 or 34.44")]
        public string ServiceCharge { get; set; }

        public virtual Businesses Company { get; set; }
   
        public virtual ICollection<Reviews> Reviews { get; set; }
    }
}
