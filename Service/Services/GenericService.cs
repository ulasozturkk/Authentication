using Core.Repository;
using Core.Service;
using Shared.Dtos;

namespace Service.Services;

public class GenericService<T, TDto> : IService<T, TDto> where T : class where TDto : class
{
    protected readonly IRepository<T> _repository;
    protected readonly IUnitOfWork _unitOfWork;

    public GenericService(IRepository<T> repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseDto<TDto>> AddAsync(TDto entity)
    {
        var newEntity = ObjectMapper.Mapper.Map<T>(entity);
        await _repository.AddAsync(newEntity);
        await _unitOfWork.CommitAsync();
        var entityDto = ObjectMapper.Mapper.Map<TDto>(newEntity);
        return ResponseDto<TDto>.Success(entityDto,200);
    }

    public async Task<ResponseDto<NoDataDto>> Delete(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null){
            return ResponseDto<NoDataDto>.Fail("entity not found",404);
        }
        return ResponseDto<NoDataDto>.Success(200);
    }

    public async Task<ResponseDto<IEnumerable<TDto>>> GetAllAsync()
    {
        var entity = ObjectMapper.Mapper.Map<List<TDto>>(await _repository.GetAllAsync());
        return ResponseDto<IEnumerable<TDto>>.Success(entity,200);
    }

    public async Task<ResponseDto<TDto>> GetByIdAsync(int id)
    {
        var entity = ObjectMapper.Mapper.Map<TDto>(await _repository.GetByIdAsync(id));
        if (entity == null){
            return ResponseDto<TDto>.Fail("Entity not found",404);
        }
        return ResponseDto<TDto>.Success(entity,200);
    }

   

    public async Task<ResponseDto<NoDataDto>> Update(TDto entity, int id)
{
    var existEntity = await _repository.GetByIdAsync(id);
    if (existEntity == null)
    {
        return ResponseDto<NoDataDto>.Fail("id not found",404);
    }
    var updatedEntity = ObjectMapper.Mapper.Map<T>(entity); // DTO'dan gelen veriyi T türüne dönüştür
    _repository.Update(updatedEntity); // Güncellenmiş varlığı veritabanına işle
    await _unitOfWork.CommitAsync(); // Değişiklikleri kaydet
    return ResponseDto<NoDataDto>.Success(204);
}
}
