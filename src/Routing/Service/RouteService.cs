using Business;
using Dto.Request;
using Dto.Response;
using System.Threading.Tasks;

namespace Service
{
    public class RouteService : IRouteService
    {

        private readonly IRouteBusiness _routeBusiness;

        public RouteService(IRouteBusiness routeBusiness)
        {
            _routeBusiness = routeBusiness;
        }

        public async Task<DtoRoutesResponse> GetAllCollectionAsync(DtoRouteRequest request)
        {
            return await _routeBusiness.GetAllRoutesAsync(request);
        }
    }
}
