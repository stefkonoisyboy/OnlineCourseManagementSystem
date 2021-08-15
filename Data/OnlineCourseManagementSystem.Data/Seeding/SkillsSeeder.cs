namespace OnlineCourseManagementSystem.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Data.Models;

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
                    //CourseId = 8,
                    CourseId = 6,
                },
                new Skill
                {
                    Text = "Database connections & some other nice features.",
                    //CourseId = 8,
                    CourseId = 6,
                },
                new Skill
                {
                    Text = "Eaque ex exercitationem quia reprehenderit?",
                    //CourseId = 8,
                    CourseId = 6,
                },
                new Skill
                {
                    Text = "Ab distinctio nemo, provident quia quibusdam ullam.",
                    //CourseId = 8,
                    CourseId = 6,
                },
                new Skill
                {
                    Text = "Conclusion & Notes.",
                    //CourseId = 8,
                    CourseId = 6,
                },
            };

            await dbContext.Skills.AddRangeAsync(skills);
            await dbContext.SaveChangesAsync();
        }
    }
}
