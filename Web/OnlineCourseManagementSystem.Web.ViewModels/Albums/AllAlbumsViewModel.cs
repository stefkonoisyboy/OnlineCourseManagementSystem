namespace OnlineCourseManagementSystem.Web.ViewModels.Albums
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllAlbumsViewModel
    {
        public IEnumerable<AlbumViewModel> Albums { get; set; }

        public AlbumInputModel InputModel { get; set; }
    }
}
