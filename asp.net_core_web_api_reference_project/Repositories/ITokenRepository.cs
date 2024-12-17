using Microsoft.AspNetCore.Identity;

namespace asp.net_core_web_api_reference_project.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);  // means the response will be an string
    }
}
