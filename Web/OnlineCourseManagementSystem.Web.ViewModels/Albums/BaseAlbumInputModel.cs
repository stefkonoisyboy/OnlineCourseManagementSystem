using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineCourseManagementSystem.Web.ViewModels.Albums
{
    public class BaseAlbumInputModel
    {
        [Required]
        public string Name { get; set; }

        public string UserId { get; set; }
    }
}
