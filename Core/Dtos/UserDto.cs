namespace Core.Dtos;

public class UserDto
{
    public string Id { get; set; }
    public string Email { get; set; }

    public string UserName { get; set; }
     public string AccessToken { get; set; }
    public DateTime AccessTokenExpiration { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiration { get; set; }
}
