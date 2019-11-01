using System;
using System.ComponentModel.DataAnnotations;

namespace TrashCollector.Models
{
    public class PickUpDayListViewModel
    {
        [Key]
        public Guid PickUpId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Scheduled Date")]
        public DateTime Scheduled { get; set; }

        [Display(Name = "Pending")]
        public bool Pending { get; set; }

        [Display(Name = "Completed")]
        public bool Completed { get; set; }

        [Display(Name = "Recurring")]
        public bool Recurring { get; set; }

        [Display(Name = "Cost")]
        public decimal Cost { get; set; }

        [Display(Name = "Paid")]
        public bool Paid { get; set; }

        public Guid CustomerId { get; set; }
        public CustomerModel Customer { get; set; }

        public Guid EmployeeId { get; set; }

    }
}