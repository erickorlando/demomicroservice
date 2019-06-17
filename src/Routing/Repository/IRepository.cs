using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRepository<T>
        where T : class
    {
        Task<ICollection<T>> GetAllCollectionAsync(ICriteria filter);
    }
}