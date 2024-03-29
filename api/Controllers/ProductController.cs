using Core.Dtos;
using Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]/[Action]")]
public class ProductController : CustomBaseController
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllProducts(string userId){
        return ActionResult(await _productService.GetUserProducts(userId));
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct(ProductDto productDto){
        return ActionResult(await _productService.AddAsync(productDto));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct(ProductDto productDto){
        return ActionResult(await _productService.Update(productDto,productDto.Id));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id) {
        return ActionResult(await _productService.Delete(id));
    }
}
