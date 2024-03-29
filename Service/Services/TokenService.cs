using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Core.Dtos;
using Core.Entities;
using Core.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Shared.Configuration;

namespace Service.Services;

public class TokenService : ITokenService
{


    private IEnumerable<Claim> GetClaims(User user)
{
    

    var claims = new List<Claim> {
        new Claim(ClaimTypes.NameIdentifier,user.Id),
        new Claim(JwtRegisteredClaimNames.Email,user.Email),
        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Aud,"www.miniapi1.com")
    };

    return claims;
}


    private string CreateRefreshToken(){
        var numberByte = new Byte[32];
        using var rnd = RandomNumberGenerator.Create();
        rnd.GetBytes(numberByte);
        return Convert.ToBase64String(numberByte);
    }

    public TokenDto CreateToken(User user)
    {
        var AccessTokenExpiration = DateTime.UtcNow.AddMinutes(300);
        var RefreshTokenExpiration = DateTime.UtcNow.AddDays(2);
        var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysecuritykeymysecuritykeymysecuritykeymysecuritykey"));

        SigningCredentials signingCredentials = new(SecurityKey,SecurityAlgorithms.HmacSha256);

        JwtSecurityToken jwtSecurityToken = new(
            issuer: "www.authserver.com",
            expires: AccessTokenExpiration,
            notBefore: DateTime.UtcNow,
            claims: GetClaims(user),
            signingCredentials: signingCredentials
        );
        var handler = new JwtSecurityTokenHandler();
        var token = handler.WriteToken(jwtSecurityToken);

        var tokenDto = new TokenDto{
            AccessToken = token,
            RefreshToken = CreateRefreshToken(),
            AccessTokenExpiration = AccessTokenExpiration,
            RefreshTokenExpiration = RefreshTokenExpiration
        };

        return tokenDto;
    }
}
