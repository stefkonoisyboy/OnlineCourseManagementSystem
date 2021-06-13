﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Channels
{
    public class CreateChannelInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(10)]
        public string Code { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public string CreatorId { get; set; }
    }
}
