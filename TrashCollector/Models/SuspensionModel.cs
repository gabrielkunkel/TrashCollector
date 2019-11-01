﻿using System;
using System.ComponentModel.DataAnnotations;

namespace TrashCollector.Models
{
    public class SuspensionModel
    {
        [Key]
        public Guid SuspensionId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "First Day of Suspension")]
        public DateTime Start { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Last Day of Suspension")]
        public DateTime End { get; set; }
        public Guid CustomerId { get; set; }

    }
}