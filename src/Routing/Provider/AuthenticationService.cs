using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using Utility;

namespace Provider
{
    public static class AuthenticationService
    {
        public static async Task<bool> ValidateAsync(this HttpRequest request, HttpResponse response, bool ignore, Appsettings settings)
        {
            if (!ignore && !request.Method.ToUpper().Equals("OPTIONS"))
            {
                if (request.Headers.ContainsKey("Authorization_App") && request.Headers["Authorization_App"].ToString().Equals(settings.Token))
                {
                    return true;
                }
                else
                {
                    response.StatusCode = Convert.ToInt32(HttpStatusCode.Unauthorized);
                    await response.WriteAsync(JsonConvert.SerializeObject("Unauthorized"));

                    return false;
                }
            }
            else
            {
                return true;
            }
        }
    }
}
