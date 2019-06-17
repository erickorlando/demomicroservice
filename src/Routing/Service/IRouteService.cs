using System.Threading.Tasks;
using Dto.Request;
using Dto.Response;

namespace Service
{
    public interface IRouteService
    {
        Task<DtoRoutesResponse> GetAllCollectionAsync(DtoRouteRequest request);
    }
}