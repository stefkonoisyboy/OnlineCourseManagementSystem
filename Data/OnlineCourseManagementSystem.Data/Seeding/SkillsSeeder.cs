using OnlineCourseManagementSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourseManagementSystem.Data.Seeding
{
    public class SkillsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Skills.Any())
            {
                return;
            }

            ICollection<Skill> skills = new List<Skill>()
            {
                new Skill
                {
                    Text = "Basics of GIT and how to become a STAR.",
                    CourseId = 8,
                },
                new Skill
                {
                    Text = "Database connections & some other nice features.",
                    CourseId = 8,
                },
                new Skill
                {
                    Text = "Eaque ex exercitationem quia reprehenderit?",
                    CourseId = 8,
                },
                new Skill
                {
                    Text = "Ab distinctio nemo, provident quia quibusdam ullam.",
                    CourseId = 8,
                },
                new Skill
                {
                    Text = "Conclusion & Notes.",
                    CourseId = 8,
                },
            };

            await dbContext.Skills.AddRangeAsync(skills);
            await dbContext.SaveChangesAsync();
        }
    }
}
