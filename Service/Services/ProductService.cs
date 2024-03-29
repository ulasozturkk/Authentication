using Core.Dtos;
using Core.Entities;
using Core.Repository;
using Core.Service;
using Microsoft.EntityFrameworkCore;
using Repository;
using Shared.Dtos;

namespace Service.Services;

public class ProductService : GenericService<Product,ProductDto>, IProductService
{

    public ProductService(IRepository<Product> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
    {
    }

    public async Task<ResponseDto<List<ProductDto>>> GetUserProducts(string userId)
    {
        
        var products = await _repository.GetAllAsync();
        var userProducts = products.Where(p=>p.OwnerId == userId).ToList();
        if(userProducts == null) {
            Console.WriteLine("aaaa");
        }
        
        var productsDto = ObjectMapper.Mapper.Map<List<ProductDto>>(products);
        return ResponseDto<List<ProductDto>>.Success(productsDto,200);

    }
}
