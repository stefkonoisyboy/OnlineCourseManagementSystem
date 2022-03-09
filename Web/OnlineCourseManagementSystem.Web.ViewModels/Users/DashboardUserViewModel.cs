namespace OnlineCourseManagementSystem.Web.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using OnlineCourseManagementSystem.Data.Models;
    using OnlineCourseManagementSystem.Services.Mapping;

    public class DashboardUserViewModel : IMapFrom<ApplicationUser>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public int Age => this.GetAge();

        private int GetAge()
        {
            return DateTime.UtcNow.Year - this.BirthDate.Year;
        }
    }
}
