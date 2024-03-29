using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Service.Services;

public class KeyService
{
    public static SecurityKey GetSymmetricSecurityKey(string SecurityKey) {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysecuritykeymysecuritykeymysecuritykeymysecuritykey"));
    }
}
