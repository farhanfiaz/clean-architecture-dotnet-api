using CommonCore.Core.Entities;
using CommonCore.InterfaceAdapters.Dtos.Auth;

namespace CommonCore.Core.DtoExtensions;

public static class AuthResponceExtension
{
    public static AuthResponceVM GenerateAuthResponce(this AspNetUser user, string jwtToken)
    {
        return new AuthResponceVM()
        {
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Token = jwtToken
        };
    }
    public static AuthToken DtoToEntity(this SingleDeviceDto model)
    {
        return new AuthToken()
        {
            CreatedAt = DateTime.UtcNow,
            DeviceId = model.DeviceId,
            DeviceName = model.DeviceName,
            DevicePlatForm = model.DevicePlatForm,
            UserName = model.UserName,
            PublicKey = model.PublicKey,
        };
    }
}
