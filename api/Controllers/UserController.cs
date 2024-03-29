using Core.Dtos;
using Core.Service;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : CustomBaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser (LoginDto loginDto) {
        return ActionResult(await _userService.CreateUserAsync(loginDto));
    }

    [HttpGet] 
    public async Task<IActionResult> Login ([FromQuery]LoginDto loginDto){
        return ActionResult(await _userService.LoginUserAsync(loginDto));
    }
}
