using System.Threading.Tasks;
using Dto.Request;
using Dto.Response;

namespace Business
{
    public interface IRouteBusiness
    {
        Task<DtoRoutesResponse> GetAllRoutesAsync(DtoRouteRequest request);
    }
}