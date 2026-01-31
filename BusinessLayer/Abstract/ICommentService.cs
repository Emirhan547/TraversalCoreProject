using DTOLayer.DTOs.CommentsDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICommentService
        : IGenericService<ResultCommentDto, CreateCommentDto, UpdateCommentDto>
    {
        Task<IReadOnlyList<ResultCommentDto>> GetByDestinationIdAsync(int destinationId);
        Task<IReadOnlyList<ResultCommentDto>> GetCommentsWithDestinationAsync();
        Task<IReadOnlyList<ResultCommentDto>> GetCommentsWithDestinationAndUserAsync(int userId);
    }
}
