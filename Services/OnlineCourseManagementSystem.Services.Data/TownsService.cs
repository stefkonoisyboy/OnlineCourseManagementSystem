using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineCourseManagementSystem.Data.Common.Repositories;
using OnlineCourseManagementSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineCourseManagementSystem.Services.Data
{
    public class TownsService : ITownsService
    {
        private readonly IDeletableEntityRepository<Town> townsRepository;

        public TownsService(IDeletableEntityRepository<Town> townsRepository)
        {
            this.townsRepository = townsRepository;
        }

        public IEnumerable<SelectListItem> GetAllAsSelectListItems()
        {
            return this.townsRepository
                .All()
                .OrderBy(t => t.Name)
                .Select(t => new SelectListItem
                {
                    Text = t.Name,
                    Value = t.Id.ToString(),
                })
                .ToList();
        }
    }
}
