using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using GameSever.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using SharedLibrary;

namespace GameSever.Services;

// Simpliest authentiaction service you can ever come across to.
public class AuthenticationService : IAuthenticationService
{

    private readonly Settings _settings;
    private readonly GameDbContext _context;

    private AuthenticationService(Settings settings, GameDbContext context)
    {
        _settings = settings;
        _context = context;
    }

    public (bool success, string content) Register(string username, string password)
    {
        if (_context.Users.Any(u => u.Username == username)) return (false, "Username not available");

        var user = new User
        {
            Username = username,
            PasswordHash = password
        };
        
        user.ProvideSaltAndHash();

        // After user created with hash it can be added to database. 
        _context.Add(user);
        _context.SaveChanges();

        return (true, "");
    }    
    public (bool success, string token) Login (string username, string password)
    {
        var user = _context.Users.SingleOrDefault(u => u.Username == username);
        if (user == null) return (false, "Invalid Username");

        // IF username is present in database, it compares password hash in database.
        if (user.PasswordHash != AuthenticationHelpers.ComputeHash(password, user.Salt))
            return (false, "Invalid password");

        return (true, GenerateJWTToken(AssembleClaimsIdentity(user)));

    }

    private ClaimsIdentity AssembleClaimsIdentity(User user)
    {
        var subject = new ClaimsIdentity(new[]
        {
            new Claim("id", user.Id.ToString())
        });
        
        
        
        return subject;
    }
    
    /// <summary>
    ///  The server generates token with all the permissions inside it and gives it to client. The server does not need
    /// to store this anywhere but can also be assured what client gives them has been authorized by server. Because server
    /// has signed it with its symmetric secret key you do not have access to that secret key. 
    /// </summary>
    /// <param name="subject"></param>
    /// <returns></returns>
    public string GenerateJWTToken(ClaimsIdentity subject)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_settings.BearerKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = subject,
            Expires = DateTime.Now.AddYears(10),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
}

public interface IAuthenticationService
{
    (bool success, string content) Register(string username, string password);
    (bool success, string token) Login(string username, string password);
        
}



/// <summary>
/// We take plain text password and we are running it through a hashing algorithm (sha-256). Hashing is a oen way function.
/// Take bit of data you hash it and get weir gobbled data making it impossible to return original vers. 
/// </summary>
public static class AuthenticationHelpers
{
    public static void ProvideSaltAndHash(this User user)
    {
        var salt = GenerateSalt();
        user.Salt = Convert.ToBase64String(salt);
        user.PasswordHash = ComputeHash(user.PasswordHash, user.Salt);

    }

    private static Byte[] GenerateSalt()
    {
        var rng = RandomNumberGenerator.Create();
        var salt = new byte[24];
        rng.GetBytes(salt);
        return salt;
    }

    public static string ComputeHash(string password, string saltString)
    {
        var salt = Convert.FromBase64String(saltString);
        using var hashGenerator = new Rfc2898DeriveBytes(password, salt);
        var bytes = hashGenerator.GetBytes(24);
        return Convert.ToBase64String(bytes);
    }
}
