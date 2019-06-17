using Dto.Request;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.Threading.Tasks;

namespace Routing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly IRouteService _routeService;

        public RoutesController(IRouteService routeService)
        {
            _routeService = routeService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync([FromBody] DtoRouteRequest request)
        {
            var lista = await _routeService.GetAllCollectionAsync(request);
            return Ok(lista);
        }

    }
}