using System;
using System.ComponentModel.DataAnnotations;

namespace TrashCollector.Models
{
    public class CustomerOwesForThisMonthViewModel
    {
        public Guid CustomerId { get; set; }

        [Display(Name = "You'll owe for this month:")]
        public decimal OwesThisMonth { get; set; }

        [Display(Name = "You owe for past pick-ups:")]
        public decimal Bill { get; set; }

    }
}