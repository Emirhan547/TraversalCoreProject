using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IGenericService <TResultDto,TCreateDto,TUpdateDto>
    {
        Task AddAsync(TCreateDto dto);
        Task UpdateAsync(TUpdateDto dto);
        Task DeleteAsync(int id);
        Task<IReadOnlyList<TResultDto>> GetListAsync();
        Task<TResultDto?> GetByIdAsync(int id);
    }
}
