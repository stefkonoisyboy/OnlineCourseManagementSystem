namespace OnlineCourseManagementSystem.Web.ViewModels.Channels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class JoinChannelInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        [Display(Name = "Event code")]
        public string Code { get; set; }

        public string UserId { get; set; }
    }
}
