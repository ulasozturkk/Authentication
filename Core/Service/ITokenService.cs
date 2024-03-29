using Core.Dtos;
using Core.Entities;

namespace Core.Service;

public interface ITokenService
{
    TokenDto CreateToken(User user);
}
