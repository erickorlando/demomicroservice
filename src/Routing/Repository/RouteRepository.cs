using Data.Context;
using Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Criteria;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public class RouteRepository : IRepository<Route>
    {
        private readonly RoutingDbContext _dbContext;

        public RouteRepository(RoutingDbContext dbContext)  
        {
            _dbContext = dbContext;
        }
        
        public async Task<ICollection<Route>> GetAllCollectionAsync(ICriteria filter)
        {
            var filtro = (RouteCriteria) filter;

            var list = await _dbContext.Query<Route>()
                .FromSql($"uspSelectAllRoutes {filtro.CountryCode}")
                .AsNoTracking()
                .ToListAsync();

            return list;
        }

    }
}
