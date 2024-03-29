using Core.Dtos;
using Core.Entities;
using Shared.Dtos;

namespace Core.Service;

public interface IProductService : IService<Product,ProductDto>
{
    Task<ResponseDto<List<ProductDto>>> GetUserProducts(string userId);
}
