using System;
using System.Collections.Generic;

namespace MoonlightGID.Models
{
    public partial class Services
    {
        public Services()
        {
           
        }
        public int ServiceId { get; set; }
        public int JobId { get; set; }
        public int CustomerId { get; set; }
        public decimal Price { get; set; }
        public string TimeDescription { get; set; }
        public string Description { get; set; }
        public string DateOrder { get; set; }
        public string ServiceLocation { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual Jobs Job { get; set; }
    }
}
