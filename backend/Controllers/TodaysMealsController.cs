#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using frontend;
using frontend.Data;

namespace frontend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodaysMealsController : ControllerBase
    {
        private readonly DataContext _context;

        public TodaysMealsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Meal
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Meal>>> GetMeals()
        {
            var freshMeal = _context.Meals
                .Where(x => x.LastDateTime.AddDays(3) < DateTime.Now.ToUniversalTime());

            var meat = freshMeal
                .Where(x => x.Type == "M")
                .OrderBy(x => Guid.NewGuid())
                .Take(2);
            var vg = freshMeal
                .Where(x => x.Type == "V")
                .OrderBy(x => Guid.NewGuid())
                .Take(3);
            var soup = freshMeal
                .Where(x => x.Type == "S")
                .OrderBy(x => Guid.NewGuid())
                .Take(1);
            var result = meat.Union(vg).Union(soup);
            return await result.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Meal>>> SaveTodaysMeals(IList<int> ids)
        {
            var meals = _context.Meals.Where(x => ids.Contains(x.Id));
            await meals.ForEachAsync(x => x.LastDateTime = DateTime.Now.ToUniversalTime());
            await _context.SaveChangesAsync();
            return await meals.ToListAsync();

        }
    }
}