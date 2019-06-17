using System.Collections.Generic;
using Dto.Common;

namespace Dto.Response
{
    public class DtoRoutesResponse : ResponseBase
    {
        public ICollection<DtoRoute> Routes { get; set; }
    }
}