using Dto.Request;
using Dto.Response;
using Microsoft.AspNetCore.Mvc;
using Provider;
using Service;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace Routing.Api.Facade
{
    /// <summary>
    /// Controlador de Rutas
    /// </summary>
    [ApiVersion("1.0")]
    [Route("[controller]/[action]/v{version:apiVersion}")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly IRouteService _routeService;

        /// <summary>
        /// Servicio de Rutas
        /// </summary>
        public RoutesController(IRouteService routeService)
        {
            _routeService = routeService;
        }

        /// <summary>
        /// Obtiene datos de las rutas
        /// </summary>
        [HttpPost, MapToApiVersion("1.0")]
        [SwaggerResponse(200, "With information", typeof(DtoRoutesResponse))]
        [SwaggerResponse(204, "Not information", typeof(object))]
        [SwaggerResponse(400, "Error in identity validations", typeof(string))]
        [SwaggerResponse(409, "Disadvantages in the processing", typeof(string))]
        public async Task<IActionResult> GetAllAsync([FromBody] DtoRouteRequest request)
        {
            try
            {
                return Ok(await _routeService.GetAllCollectionAsync(request));
            }
            catch (Exception ex)
            {
                throw new ExceptionService(ex);
            }
        }

    }
}