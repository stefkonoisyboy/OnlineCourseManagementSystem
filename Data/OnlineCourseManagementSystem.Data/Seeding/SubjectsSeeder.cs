namespace OnlineCourseManagementSystem.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Data.Models;

    public class SubjectsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Subjects.Any())
            {
                return;
            }

            ICollection<Subject> subjects = new List<Subject>()
            {
                new Subject
                {
                    Name = "Bulgarian",
                    Description = "One of the most important subjects in school as you have to know your own language.",
                },
                new Subject
                {
                    Name = "Mathematics",
                    Description = "Develops your logical thinking.",
                },
                new Subject
                {
                    Name = "English",
                    Description = "Develops your foreign language knowledge.",
                },
                new Subject
                {
                    Name = "Biology",
                    Description = "Develops your knowledge about animals, plants, humans and other organisms.",
                },
                new Subject
                {
                    Name = "Chemistry",
                    Description = "Develops your knowledge about chemical processes.",
                },
                new Subject
                {
                    Name = "Physics",
                    Description = "It's close to Maths and you will a lot of regularities.",
                },
                new Subject
                {
                    Name = "Philosophy",
                    Description = "You will talk about philosophers, human rights and so on.",
                },
                new Subject
                {
                    Name = "P.E.",
                    Description = "You will play some kind of sport.",
                },
                new Subject
                {
                    Name = "I.T.",
                    Description = "Develops your computer skills.",
                },
                new Subject
                {
                    Name = "Informatics",
                    Description = "Develops your coding skills and alghorithmical thinking.",
                },
                new Subject
                {
                    Name = "History",
                    Description = "You will learn about ancient civilizations.",
                },
                new Subject
                {
                    Name = "Geography",
                    Description = "You will learn about economy and nature.",
                },
                new Subject
                {
                    Name = "Art",
                    Description = "You will learn to draw.",
                },
                new Subject
                {
                    Name = "Music",
                    Description = "You will learn to sing.",
                },
            };

            await dbContext.AddRangeAsync(subjects);
            await dbContext.SaveChangesAsync();
        }
    }
}
