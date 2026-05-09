using CommonCore.Core.Entities;

namespace CommonCore.Core.Interfaces;

public interface IJwtService
{
    string GenerateToken(AspNetUser user);
}
