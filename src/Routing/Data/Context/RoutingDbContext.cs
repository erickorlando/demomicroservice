using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class RoutingDbContext : DbContext
    {
        public RoutingDbContext(DbContextOptions<RoutingDbContext> options)
        : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}