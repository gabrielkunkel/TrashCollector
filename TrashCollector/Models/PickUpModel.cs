using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrashCollector.Models
{
    public class PickUpModel
    {
        [Key]
        public Guid PickUpId { get; set; }

        [Display(Name = "Scheduled Date")]
        public DateTime Scheduled { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Cost")]
        public decimal Cost { get; set; }

        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
        public CustomerModel Customer { get; set; }
    }
}