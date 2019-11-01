using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrashCollector.Models
{
    public class CustomerOwesForThisMonthViewModel
    {
        public Guid CustomerId { get; set; }

        [Display(Name = "You'll owe for this month")]
        public decimal OwesThisMonth { get; set; }

    }
}