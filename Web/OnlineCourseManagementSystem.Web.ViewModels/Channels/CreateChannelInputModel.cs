namespace OnlineCourseManagementSystem.Web.ViewModels.Channels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class CreateChannelInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        [Display(Name = "Event code")]
        public string Code { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        [Display(Name = "Event name")]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public string CreatorId { get; set; }
    }
}
