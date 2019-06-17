using Polly;
using System;
using System.Threading.Tasks;

namespace Provider
{
    public static class TransactionService
    {
        public static async Task<T> ExecuteAsync<T>(this Task<T> task, int intent, int time)
        {
            return await Policy.Handle<Exception>().WaitAndRetryAsync(intent, i => TimeSpan.FromMilliseconds(time)).ExecuteAsync( async ()=>
            {
                return await task;
            });
        }
    }
}
