using OnlineCourseManagementSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourseManagementSystem.Data.Seeding
{
    public class TagsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Tags.Any())
            {
                return;
            }

            ICollection<Tag> tags = new List<Tag>()
            {
                new Tag
                {
                    Name = "Geometry",
                },
                new Tag
                {
                    Name = "Algebra",
                },
                new Tag
                {
                    Name = "Web",
                },
                new Tag
                {
                    Name = "AI",
                },
                new Tag
                {
                    Name = "QA",
                },
            };

            await dbContext.Tags.AddRangeAsync(tags);
            await dbContext.SaveChangesAsync();
        }
    }
}
