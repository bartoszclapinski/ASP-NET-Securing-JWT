using System.Security.Cryptography;
using ASP_NET_Securing_JWT.Users;
using Microsoft.AspNetCore.Mvc;

namespace ASP_NET_Securing_JWT.Controllers;


[Route("/api/auth/")]
[ApiController]
public class AuthController : ControllerBase
{
    public static User user = new User();

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<User>> RegisterUser(UserDto user)
    {
        
    }
}