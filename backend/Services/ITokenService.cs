using Microsoft.AspNetCore.Identity;

namespace backend.Services
{
    public interface ITokenService
    {
        string CreateToken(string email, IList<string> roles);
    }
}
