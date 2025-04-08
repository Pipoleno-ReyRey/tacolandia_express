using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class Jwt{

    public static string CreateJWT(int? Id, string? Username, string? Email, string? Phone, string? Password, string? Role){
        var secretKey = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build().GetSection("jwt").GetSection("secretKey").Value;

        var claims = new List<Claim>(){
            new Claim("id", Id.ToString()!),
            new Claim("username", Username!),
            new Claim("email", Email!),
            new Claim("phone", Phone!),
            new Claim("password", Password!),
            new Claim("role", Role!)
        };

        var secretKey1 = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
        var signing = new SigningCredentials(secretKey1, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signing,
            expires: DateTime.Now.AddDays(1)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}