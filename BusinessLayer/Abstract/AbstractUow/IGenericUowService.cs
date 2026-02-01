using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract.AbstractUow
{
    public interface IGenericUowService<TCreateDto, TUpdateDto, TResultDto>
    {
        Task AddAsync(TCreateDto dto);
        Task UpdateAsync(TUpdateDto dto);
        Task UpdateRangeAsync(List<TUpdateDto> dtos);
        Task<TResultDto?> GetByIdAsync(int id);
    }
}
 