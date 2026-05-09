using System;
using CommonCore.InterfaceAdapters.Dtos;
using CommonCore.InterfaceAdapters.Dtos.Auth;

namespace CommonCore.Core.Interfaces;

public interface IAuthTokenRepository
{
    Task<ApiResult> SaveAuthToken(SingleDeviceDto model,CancellationToken cancellationToken);
    Task<ApiResult> DeleteAuthToken(string UserId,CancellationToken cancellationToken);
}
