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

    public class ParentConfigurations : IEntityTypeConfiguration<Parent>
    {
        public void Configure(EntityTypeBuilder<Parent> builder)
        {
            builder
                .HasOne(x => x.User)
                .WithOne(x => x.Parent)
                .HasForeignKey<Parent>(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
