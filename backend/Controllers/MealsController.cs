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
    public class MealsController : ControllerBase
    {
        private readonly DataContext _context;

        public MealsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Meal
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Meal>>> GetMeals()
        {
            return await _context.Meals.ToListAsync();
        }
        // GET: api/Meal
        //[HttpGet("{dateRange}")]
        //public async Task<ActionResult<IEnumerable<Meal>>> GetTodaysMeals(string dateRange)
        //{
        //    if(dateRange == "day")
        //    {
        //        return await _context.Meals
        //            .Where(x=> x.LastDateTime.AddDays(3) < DateTime.Now.ToUniversalTime())
        //            .ToListAsync();
        //    }else if (dateRange == "week")
        //    {
        //        return await _context.Meals
        //            .Where(x=> x.LastDateTime.AddDays(3) < DateTime.Now.ToUniversalTime())
        //            .ToListAsync();
        //    }
        //    else
        //    {
        //        return await _context.Meals.ToListAsync();
        //    }
        //}

        // GET: api/Meal/5
        [HttpGet("{id}")]
        //[Route("meal/{id:int}")]
        public async Task<ActionResult<Meal>> GetMeal(int id)
        {
            var meal = await _context.Meals.FindAsync(id);

            if (meal == null)
            {
                return NotFound();
            }

            return meal;
        }

        // PUT: api/Meal/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeal(int id, Meal meal)
        {
            if (id != meal.Id)
            {
                return BadRequest();
            }

            _context.Entry(meal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MealExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Meal
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Meal>> PostMeal(Meal meal)
        {
            _context.Meals.Add(meal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMeal", new { id = meal.Id }, meal);
        }

        // DELETE: api/Meal/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeal(int id)
        {
            var meal = await _context.Meals.FindAsync(id);
            if (meal == null)
            {
                return NotFound();
            }

            _context.Meals.Remove(meal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MealExists(int id)
        {
            return _context.Meals.Any(e => e.Id == id);
        }
    }
}
