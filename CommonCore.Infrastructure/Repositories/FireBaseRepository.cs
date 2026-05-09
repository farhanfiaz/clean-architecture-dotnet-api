using System;
using CommonCore.Core.DtoExtensions;
using CommonCore.Core.Interfaces;
using CommonCore.Infrastructure.Data;
using CommonCore.InterfaceAdapters.Dtos;
using CommonCore.InterfaceAdapters.Dtos.Auth.FireBase;
using Microsoft.EntityFrameworkCore;

namespace CommonCore.Infrastructure.Repositories;

public class FireBaseRepository(CommonCoreDbContext _dbContext) : IFireBaseRepository
{
    public async Task<ApiResult> AddFireBaseToken(FirebaseTokenVM model, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(model.UserId))
        {
            return ApiResult.Fail("UserId is required");
        }
        if (string.IsNullOrWhiteSpace(model.Token))
        {
            return ApiResult.Fail("Token is required");
        }
        if (!string.IsNullOrWhiteSpace(model.PreviousToken))
        {
            var oldToken = await _dbContext.FireBaseTokens.FirstOrDefaultAsync(x => x.Token == model.PreviousToken);
            if (oldToken is not null)
            {
                return ApiResult.Fail("Token already exist");
            }
        }
        var fireBaseToken = await _dbContext.FireBaseTokens.FirstOrDefaultAsync(x => x.UserIdFk == model.UserId && x.Token == model.Token);
        if (fireBaseToken is not null)
        {
            return ApiResult.Fail("Token already exist");
        }
        await _dbContext.FireBaseTokens.AddAsync(model.DtoToEntity(), cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return ApiResult.Success();
    }
}
