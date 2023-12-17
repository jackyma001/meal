#nullable disable

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
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
    public class MealsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<MealsController> _logger; // Add ILogger

        public MealsController(DataContext context, IWebHostEnvironment env, ILogger<MealsController> logger)
        {
            _context = context;
            _env = env;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Meal>>> GetMeals()
        {
            _logger.LogInformation("Getting all meals.");
            try
            {
                return await _context.Meals.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting meals.");
                throw; // Optionally, you can return a specific error response.
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Meal>> GetMeal(int id)
        {
            _logger.LogInformation($"Getting meal with id {id}.");
            try
            {
                var meal = await _context.Meals.FindAsync(id);

                if (meal == null)
                {
                    _logger.LogWarning($"Meal with id {id} not found.");
                    return NotFound();
                }

                return meal;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting meal with id {id}.");
                throw; // Optionally, you can return a specific error response.
            }
        }

        // Other endpoints follow the same pattern...

        [HttpPost]
        public async Task<ActionResult<Meal>> PostMeal(Meal meal)
        {
            _logger.LogInformation("Creating a new meal.");
            try
            {
                _context.Meals.Add(meal);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Meal with id {meal.Id} created.");

                return CreatedAtAction("GetMeal", new { id = meal.Id }, meal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a new meal.");
                throw; // Optionally, you can return a specific error response.
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeal(int id)
        {
            _logger.LogInformation($"Deleting meal with id {id}.");
            try
            {
                var meal = await _context.Meals.FindAsync(id);
                if (meal == null)
                {
                    _logger.LogWarning($"Meal with id {id} not found.");
                    return NotFound();
                }

                _context.Meals.Remove(meal);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Meal with id {id} deleted.");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting meal with id {id}.");
                throw; // Optionally, you can return a specific error response.
            }
        }

        // Add logging for other endpoints...

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            _logger.LogInformation("Saving file.");
            try
            {
                // Existing SaveFile logic...

                return new JsonResult("File saved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while saving file.");
                throw; // Optionally, you can return a specific error response.
            }
        }

        private bool MealExists(int id)
        {
            _logger.LogInformation($"Checking if meal with id {id} exists.");
            return _context.Meals.Any(e => e.Id == id);
        }
    }
}
