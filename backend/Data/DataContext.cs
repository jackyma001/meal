
using Microsoft.EntityFrameworkCore;

namespace frontend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }

        public DbSet<Meal> Meals => Set<Meal>();
    }
}
