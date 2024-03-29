using Microsoft.AspNetCore.Identity;

namespace Core.Entities;

public class User 
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string UserName { get; set; }
    public ICollection<Product> Products { get; set; }
    
}
