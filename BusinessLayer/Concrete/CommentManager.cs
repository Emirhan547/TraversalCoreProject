using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.UnitOfWork;
using DTOLayer.DTOs.CommentsDtos;
using EntityLayer.Concrete;
using Mapster;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CommentManager : ICommentService
    {
        private readonly ICommentDal _commentDal;
        private readonly IUowDal _uowDal;

        public CommentManager(ICommentDal commentDal, IUowDal uowDal)
        {
            _commentDal = commentDal;
            _uowDal = uowDal;
        }

        public async Task AddAsync(CreateCommentDto dto)
        {
            var entity = dto.Adapt<Comment>();
            await _commentDal.AddAsync(entity);
            await _uowDal.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateCommentDto dto)
        {
            var entity = await _commentDal.GetByIdAsync(dto.Id);
            if (entity == null)
                throw new KeyNotFoundException("Comment not found");

            dto.Adapt(entity); // mevcut entity üstüne map
             _commentDal.UpdateAsync(entity);
            await _uowDal.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _commentDal.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException("Comment not found");

             _commentDal.DeleteAsync(entity);
            await _uowDal.SaveChangesAsync();
        }

        public async Task<ResultCommentDto?> GetByIdAsync(int id)
        {
            var entity = await _commentDal.GetByIdAsync(id);
            return entity?.Adapt<ResultCommentDto>();
        }

        public async Task<IReadOnlyList<ResultCommentDto>> GetListAsync()
        {
            var entities = await _commentDal.GetListAsync();
            return entities.Adapt<IReadOnlyList<ResultCommentDto>>();
        }

        public async Task<IReadOnlyList<ResultCommentDto>> GetByDestinationIdAsync(int destinationId)
        {
            var entities = await _commentDal.GetByDestinationIdAsync(destinationId);
            return entities.Adapt<IReadOnlyList<ResultCommentDto>>();
        }

        public async Task<IReadOnlyList<ResultCommentWithDetailsDto>> GetCommentsWithDestinationAsync()
        {
            var entities = await _commentDal.GetCommentsWithDestinationAsync();
            return entities.Select(entity =>
            {
                var dto = entity.Adapt<ResultCommentWithDetailsDto>();
                dto.DestinationCity = entity.Destination?.City;
                return dto;
            }).ToList();
        }

        public async Task<IReadOnlyList<ResultCommentWithDetailsDto>> GetCommentsWithDestinationAndUserAsync(int userId)
        {
            var entities = await _commentDal.GetCommentsWithDestinationAndUserAsync(userId);
            return entities.Select(entity =>
            {
                var dto = entity.Adapt<ResultCommentWithDetailsDto>();
                dto.DestinationCity = entity.Destination?.City;
                if (entity.AppUser != null)
                {
                    dto.AppUserName = entity.AppUser.Name;
                    dto.AppUserSurname = entity.AppUser.Surname;
                    dto.AppUserImageUrl = entity.AppUser.ImageUrl;
                }

                return dto;
            }).ToList();
        }

        public async Task<int> GetCountByDestinationIdAsync(int destinationId)
        {
            return await _commentDal.GetCountByDestinationIdAsync(destinationId);
        }
    }
}
