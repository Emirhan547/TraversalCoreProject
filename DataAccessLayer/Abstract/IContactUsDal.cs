using EntityLayer.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IContactUsDal : IGenericDal<ContactUs>
    {
        Task<IReadOnlyList<ContactUs>> GetListContactUsByTrueAsync();
        Task<IReadOnlyList<ContactUs>> GetListContactUsByFalseAsync();
    }
}
