using System.Security.Cryptography;
using System.Text;
using Core.Dtos;
using Core.Entities;
using Core.Repository;
using Core.Service;
using Microsoft.AspNetCore.Identity;
using Repository;
using Shared.Dtos;

namespace Service.Services;

public class UserService : IUserService
{

    private readonly AppDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly IRepository<User> _repository;
    public UserService(AppDbContext context, ITokenService tokenService, IUnitOfWork unitOfWork, IRepository<User> repository)
    {
        _context = context;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
        _repository = repository;
    }
    private void createPasswordHash(string password,out byte[] passwordHash,out byte[] passwordSalt){
        using(var hmac = new HMACSHA256()) {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    private string CalculateSHA256(string input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = sha256.ComputeHash(inputBytes);

            // Convert the byte array to a hexadecimal string.
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                builder.Append(hashBytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
    public async Task<ResponseDto<UserDto>> CreateUserAsync(LoginDto loginDto)
    {
        if (loginDto == null) throw new ArgumentNullException(nameof(loginDto));

        var hashedPassword = CalculateSHA256(loginDto.Password);

        var user = new User {Email = loginDto.Email,UserName = loginDto.UserName,PasswordHash = hashedPassword,Id = Guid.NewGuid().ToString()};
        var token = _tokenService.CreateToken(user);
        
        
        var dtoUser = new UserDto {
            Email = user.Email,
            Id = user.Id,
            UserName = user.UserName,
            AccessToken = token.AccessToken,
            AccessTokenExpiration = token.AccessTokenExpiration,
            RefreshToken = token.RefreshToken,
            RefreshTokenExpiration = token.RefreshTokenExpiration
        };
        var newUser = new User{
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            PasswordHash = user.PasswordHash,
        };

        await _repository.AddAsync(newUser);
        await _unitOfWork.CommitAsync();
        return ResponseDto<UserDto>.Success(dtoUser,200);

    }

    public async Task<ResponseDto<UserDto>> LoginUserAsync(LoginDto loginDto)
{
    if (loginDto == null) 
        throw new ArgumentNullException(nameof(loginDto));

    var user = _repository.Where(u => u.Email == loginDto.Email).SingleOrDefault();

    if(user == null) 
        return ResponseDto<UserDto>.Fail("User not found", 404);
    
    // Kullanıcının veritabanında kayıtlı olan hash ve salt değerlerini kullanarak giriş yapılmalı
    var hashedPassword = CalculateSHA256(loginDto.Password);
    if ( hashedPassword != user.PasswordHash){
        return ResponseDto<UserDto>.Fail("Email or password is wrong", 400);
    }
        
    var token = _tokenService.CreateToken(user);

    var dtoUser = new UserDto {
        Email = user.Email,
        Id = user.Id,
        AccessToken = token.AccessToken,
        AccessTokenExpiration = token.AccessTokenExpiration,
        RefreshToken = token.RefreshToken,
        RefreshTokenExpiration = token.RefreshTokenExpiration
    };

    return ResponseDto<UserDto>.Success(dtoUser, 200);
}

}
