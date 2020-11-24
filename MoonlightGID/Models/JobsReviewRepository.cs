using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoonlightGID.Models
{
    public class JobsReviewRepository
    {
        
        public List<Reviews> Reviews { get; set; }
        public List<Jobs> Jobs { get; set; }

        public List<Services> CustomerServices { get; set; }

        public Customers Customers { get; set; }

        public Services Service { get; set; }

        public List<int>ToCompare { get; set; }

        public List<string> ServiceAvailability { get; set; }

        public double GetRating(List<Reviews> r,int id)
        {
            double result = 0;
            int count = 0;
            double total=0;
            foreach(Reviews review in r)
            {
                if (review.JobId == id)
                {
                    total += review.Rating;
                    count++;
                }
                else
                {
                    continue;
                }
            }
            if(count <= 0)
               result = 0;
            else
                result = total / count;

            return result;
        }

       


    }
}
