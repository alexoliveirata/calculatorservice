using Microsoft.EntityFrameworkCore;

namespace Data.Repository.Models
{
    internal class CalculatorContext : DbContext
    {
        public CalculatorContext(DbContextOptions<CalculatorContext> options) : base(options)
        {
        }

        public DbSet<Operations> Operations { get; set; }
    }
}