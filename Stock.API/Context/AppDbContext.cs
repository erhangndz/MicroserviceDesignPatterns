using Microsoft.EntityFrameworkCore;

namespace Stock.API.Context
{
    public class AppDbContext(DbContextOptions options): DbContext(options)
    {

       public DbSet<Entities.Stock> Stocks { get; set; }
    }
}
