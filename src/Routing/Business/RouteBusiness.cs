using Dto.Request;
using Dto.Response;
using Entity;
using Repository;
using Repository.Criteria;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business
{
    public class RouteBusiness : IRouteBusiness
    {
        private readonly IRepository<Route> _repository;

        public RouteBusiness(IRepository<Route> repository)
        {
            _repository = repository;
        }

        public async Task<DtoRoutesResponse> GetAllRoutesAsync(DtoRouteRequest request)
        {
            var response = new DtoRoutesResponse();
            try
            {
                var listRoutes = await _repository.GetAllCollectionAsync(new RouteCriteria
                {
                    CountryCode = request.CountryCode
                });

                response.Routes = new List<DtoRoute>();
                foreach (var route in listRoutes)
                {
                    response.Routes.Add(new DtoRoute
                    {
                        Code = route.Code,
                        Name = route.Name,
                        Color = route.Color
                    });
                }

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

    }
}
