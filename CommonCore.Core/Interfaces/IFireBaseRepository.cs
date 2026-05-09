using System;
using CommonCore.InterfaceAdapters.Dtos;
using CommonCore.InterfaceAdapters.Dtos.Auth.FireBase;

namespace CommonCore.Core.Interfaces;

public interface IFireBaseRepository
{
    Task<ApiResult> AddFireBaseToken(FirebaseTokenVM model, CancellationToken cancellationToken);
}
