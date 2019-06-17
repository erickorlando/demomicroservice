using Newtonsoft.Json.Linq;
using System;

namespace Utility
{
    public class Response : IDisposable
    {
        public int Code { get; set; }
        public JToken Message { get; set; }
        public JToken Body { get; set; }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
