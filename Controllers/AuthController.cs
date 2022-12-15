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

    private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
        
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<User>> RegisterUser(UserDto newUser)
    {
        CreatePasswordHash(newUser.Password, out byte[] passwordHash, out byte[] passwordSalt);

        user.Username = newUser.Username;
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(UserDto userlogin)
    {
        if (user.Username != userlogin.Username)
        {
            return BadRequest("Username not exists.");
        }

        if (!VerifyPassword(userlogin.Password, user.PasswordHash, user.PasswordSalt))
        {
            return BadRequest("Wrong password.");
        }

        return Ok("MY_TOKEN");
    }
}