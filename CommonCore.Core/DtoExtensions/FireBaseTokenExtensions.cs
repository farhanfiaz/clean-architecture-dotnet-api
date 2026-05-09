using System;
using CommonCore.Core.Entities;
using CommonCore.InterfaceAdapters.Dtos.Auth.FireBase;

namespace CommonCore.Core.DtoExtensions;

public static class FireBaseTokenExtensions
{
    public static FireBaseToken DtoToEntity(this FirebaseTokenVM model)
    {
        return new FireBaseToken(){
        CreatedAt=DateTime.UtcNow,
        DeviceName=model.DeviceName,
        DeviceType=model.DeviceType,
        Token=model.Token,
        UserIdFk=model.UserId,
        };
    }
}
