using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Utility;

namespace Provider
{
    public static class ResponseService
    {
        public static async Task TransformAsync(this HttpResponse response, HttpContext context, RequestDelegate next, bool ignore)
        {
            if (!ignore)
            {
                var existingBody = response.Body;

                using (MemoryStream newBody = new MemoryStream())
                using (Response body = new Response())
                {
                    try
                    {
                        response.Body = newBody;

                        await next(context);

                        response.Body.Seek(0, SeekOrigin.Begin);
                        response.Body = existingBody;

                        body.Code = 0;

                        if (response.StatusCode == 400)
                        {
                            body.Message = JToken.Parse(await new StreamReader(newBody).ReadToEndAsync());
                            body.Body = "";
                        }
                        else
                        {
                            body.Message = "";
                            body.Body = JToken.Parse(await new StreamReader(newBody).ReadToEndAsync());
                        }
                    }
                    catch(ExceptionService ex)
                    {
                        body.Code = Convert.ToInt32(ex.InnerException.HelpLink);
                        body.Message = JToken.Parse(" {'Error': 'asdsa'} ");
                        body.Body = "";

                        response.StatusCode = Convert.ToInt32(HttpStatusCode.Conflict);
                    }
                    finally
                    {
                        await response.WriteAsync(JsonConvert.SerializeObject(body));
                    }
                }
            }
            else
            {
                await next(context);
            }
        }
    }
}
