﻿using System;
using System.ComponentModel.DataAnnotations;

namespace TrashCollector.Models
{
    public class AddressModel
    {
        [Key]
        public Guid AddressId { get; set; }

        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        [Display(Name = "Secondary Address")]
        public string SecondaryAddress { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public USState State { get; set; }

        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

    }
}