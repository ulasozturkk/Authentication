using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomBaseController : ControllerBase
{
    public IActionResult ActionResult<T>(ResponseDto<T> response) where T: class {
        return new ObjectResult(response){
            StatusCode = response.StatusCode
        };
    }
}
