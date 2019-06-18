using System.Collections.Generic;
using System.Threading.Tasks;
using Entity;
using Provider;
using Repository.Criteria;
using Repository.Factory;
using Utility;

namespace Repository.Adapters
{
    public class RouteSqlRepository : IRepository<Route>
    {
        private readonly SqlServerService _sqlServerService;

        public RouteSqlRepository()
        {
            _sqlServerService = SqlServiceFactory.CreateService();
        }

        public async Task<ICollection<Route>> GetAllCollectionAsync(ICriteria filter)
        {
            var filtro = (RouteCriteria)filter;

            var param = new Parameter
            {
                key = "@CountryCode",
                value = filtro.CountryCode
            };

            return await _sqlServerService
                .TransaccionAsync<Route>("uspSelectAllRoutes",
                    new List<Parameter> { param });
        }
    }
}