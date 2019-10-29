using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrashCollector.Models
{
    public enum CustomerStatus
    {
        Active,
        Suspended,
        Canceled,
        Blacklisted,
        Priority
    }

    public class CustomerModel
    {
        [Key]
        public Guid CustomerId { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        [Display(Name = "Status")]
        public CustomerStatus Status { get; set; }

        [Display(Name = "Weekly Pick-Up Day")]
        public DayOfWeek PickUpDay { get; set; }

        [Display(Name = "Cost Per Pick-Up")]
        public Decimal BaseCost { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("Address")]
        public Guid AddressId { get; set; }
        public AddressModel Address { get; set; }
    }
}