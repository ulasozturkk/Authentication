using Core.Dtos;
using Shared.Dtos;

namespace Core.Service;

public interface IUserService
{
    Task<ResponseDto<UserDto>> CreateUserAsync(LoginDto loginDto);

    Task<ResponseDto<UserDto>> LoginUserAsync(LoginDto loginDto);
}
