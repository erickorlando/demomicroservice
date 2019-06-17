using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;
using Utility;

namespace Provider
{
    public class MiddlewareInterceptor
    {
        private readonly RequestDelegate _next;
        private readonly Appsettings _settings;

        public MiddlewareInterceptor(RequestDelegate next, IOptions<Appsettings> settings)
        {
            _next = next;
            _settings = settings.Value;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            bool ignore = _settings.PathIgnore.Any(p => context.Request.Path.StartsWithSegments(p));
            bool security = await context.Request.AnalizerAsync();
            bool token = await context.Request.ValidateAsync(context.Response, ignore, _settings);

            if (security && token) await context.Response.TransformAsync(context, _next, ignore);
        }
    }
}
