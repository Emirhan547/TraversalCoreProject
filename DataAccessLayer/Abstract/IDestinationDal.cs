using EntityLayer.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IDestinationDal : IGenericDal<Destination>
    {
        Task<Destination?> GetDestinationWithGuideAsync(int id);
        Task<IReadOnlyList<Destination>> GetLast4DestinationsAsync();
    }
}
