using Shared.Dtos;

namespace Core.Service;

public interface IService<T,TDto> where T: class where TDto: class
{
    Task<ResponseDto<TDto>> GetByIdAsync(int id);

    Task<ResponseDto<IEnumerable<TDto>>> GetAllAsync();

    Task<ResponseDto<TDto>> AddAsync(TDto entity);

    Task<ResponseDto<NoDataDto>> Delete(int id);

    Task<ResponseDto<NoDataDto>> Update(TDto entity, int id);

}
