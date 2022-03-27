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
            return await _context.Meals
                .Where(x => x.LastDateTime.AddDays(3) < DateTime.Now.ToUniversalTime())
                .OrderBy(x=>Guid.NewGuid())
                .Take(3)
                .ToListAsync();
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
    }

}