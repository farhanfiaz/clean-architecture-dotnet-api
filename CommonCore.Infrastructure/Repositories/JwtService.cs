using CommonCore.Core.Entities;
using CommonCore.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CommonCore.Infrastructure.Repositories;

public class JwtService(IConfiguration _iConfiguration) : IJwtService
{
    public string GenerateToken(AspNetUser user)
    {
        Claim[] claims = [
             new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            new Claim(ClaimTypes.Name,user.UserName.ToString()) ,
    new Claim(ClaimTypes.Email,user.Email.ToString())];

        var secretKey = _iConfiguration.GetSection("jwt:SecretKey")?.Value?.ToString() ?? string.Empty;
        
        var securityKey = System.Text.Encoding.UTF8.GetBytes(secretKey);
        var symetricKey = new SymmetricSecurityKey(securityKey);
        var signingCredentials = new SigningCredentials(symetricKey, SecurityAlgorithms.HmacSha256);
        var jwtSecurityToken = new JwtSecurityToken(signingCredentials: signingCredentials,
        issuer: _iConfiguration.GetSection("jwt:Isuser")?.Value,
        audience:_iConfiguration.GetSection("jwt:Isuser")?.Value
            , expires: DateTime.UtcNow.AddMinutes(Convert.ToInt16(_iConfiguration.GetSection("jwt:ExpireInMinutes")?.Value))
            , claims: claims);
        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }
}
