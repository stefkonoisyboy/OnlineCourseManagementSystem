namespace OnlineCourseManagementSystem.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using OnlineCourseManagementSystem.Data.Models;

    public class TownsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Towns.Any())
            {
                return;
            }

            ICollection<Town> towns = new List<Town>()
            {
                new Town
                {
                    Name = "Sofia",
                },
                new Town
                {
                    Name = "Plovdiv",
                },
                new Town
                {
                    Name = "Varna",
                },
                new Town
                {
                    Name = "Burgas",
                },
                new Town
                {
                    Name = "Ruse",
                },
                new Town
                {
                    Name = "Vidin",
                },
                new Town
                {
                    Name = "Pleven",
                },
                new Town
                {
                    Name = "Blagoevgrad",
                },
                new Town
                {
                    Name = "Kardzhali",
                },
                new Town
                {
                    Name = "Smolyan",
                },
                new Town
                {
                    Name = "Targovishte",
                },
                new Town
                {
                    Name = "Dobrich",
                },
                new Town
                {
                    Name = "Lovech",
                },
                new Town
                {
                    Name = "Montana",
                },
                new Town
                {
                    Name = "Vratsa",
                },
                new Town
                {
                    Name = "Pernik",
                },
                new Town
                {
                    Name = "Kustendil",
                },
                new Town
                {
                    Name = "Pazardzhik",
                },
                new Town
                {
                    Name = "Haskovo",
                },
                new Town
                {
                    Name = "Yambol",
                },
                new Town
                {
                    Name = "Sliven",
                },
                new Town
                {
                    Name = "Stara Zagora",
                },
                new Town
                {
                    Name = "Gabrovo",
                },
                new Town
                {
                    Name = "Veliko Tarnovo",
                },
                new Town
                {
                    Name = "Razgrad",
                },
                new Town
                {
                    Name = "Shumen",
                },
                new Town
                {
                    Name = "Silistra",
                },
            };

            await dbContext.AddRangeAsync(towns);
            await dbContext.SaveChangesAsync();
        }
    }
}
