#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; // Add this for ILogger
using frontend;
using frontend.Data;

namespace frontend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodaysMealsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ILogger<TodaysMealsController> _logger; // Add ILogger

        public TodaysMealsController(DataContext context, ILogger<TodaysMealsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Meal>>> GetMeals()
        {
            _logger.LogInformation("Getting today's meals.");
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting today's meals.");
                throw; // Optionally, you can return a specific error response.
            }
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Meal>>> SaveTodaysMeals(IList<int> ids)
        {
            _logger.LogInformation("Saving today's meals.");
            try
            {
                var meals = _context.Meals.Where(x => ids.Contains(x.Id));
                await meals.ForEachAsync(x => x.LastDateTime = DateTime.Now.ToUniversalTime());
                await _context.SaveChangesAsync();

                _logger.LogInformation("Today's meals saved.");

                return await meals.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while saving today's meals.");
                throw; // Optionally, you can return a specific error response.
            }
        }
    }
}
