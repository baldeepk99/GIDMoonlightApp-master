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
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime DateOrder { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual Jobs Job { get; set; }
    }
}
