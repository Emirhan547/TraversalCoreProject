using EntityLayer.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ICommentDal : IGenericDal<Comment>
    {
        Task<IReadOnlyList<Comment>> GetByDestinationIdAsync(int destinationId);
        Task<IReadOnlyList<Comment>> GetCommentsWithDestinationAsync();
        Task<IReadOnlyList<Comment>> GetCommentsWithDestinationAndUserAsync(int userId);
    }
}
