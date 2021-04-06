namespace OnlineCourseManagementSystem.Data.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OnlineCourseManagementSystem.Data.Models;

    public class LecturerConfigurations : IEntityTypeConfiguration<Lecturer>
    {
        public void Configure(EntityTypeBuilder<Lecturer> builder)
        {
            builder
                .HasOne(x => x.User)
                .WithOne(x => x.Lecturer)
                .HasForeignKey<Lecturer>(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
