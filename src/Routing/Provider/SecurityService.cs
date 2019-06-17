using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Provider
{
    public static class SecurityService
    {
        public static async Task<bool> AnalizerAsync(this HttpRequest request)
        {
            return await Task<bool>.Factory.StartNew(() =>
            {
                return true;
            });
        }
    }
}
