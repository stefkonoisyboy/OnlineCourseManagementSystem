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

    public class CourseConfigurations : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder
                .HasOne(x => x.File)
                .WithOne(x => x.Course)
                .HasForeignKey<Course>(x => x.FileId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
