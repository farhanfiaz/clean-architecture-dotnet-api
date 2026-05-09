using CommonCore.Core.DtoExtensions;
using CommonCore.Core.Entities;
using CommonCore.Core.Interfaces;
using CommonCore.Infrastructure.Data;
using CommonCore.InterfaceAdapters.Dtos;
using CommonCore.InterfaceAdapters.Dtos.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CommonCore.Infrastructure.Repositories;

public class AuthTokenRepository(CommonCoreDbContext _dbContext
, IAuthRepository _iAuthRepo, UserManager<AspNetUser> _userManager)
 : IAuthTokenRepository
{
    public async Task<ApiResult> DeleteAuthToken(string UserId, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(UserId))
        {
            return ApiResult.Fail("UserId is required");
        }
        var UserInfo = await _userManager.FindByIdAsync(UserId);
        if (UserInfo is null)
        {
            return ApiResult.Fail("User not found");
        }
        var authTokenList = await _dbContext.AuthTokens.Where(x => x.UserName == UserInfo.UserName).ToListAsync(cancellationToken);
        if (authTokenList != null && authTokenList.Count > 0)
        {
            _dbContext.AuthTokens.RemoveRange(authTokenList);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        return ApiResult.Success();
    }

    public async Task<ApiResult> SaveAuthToken(SingleDeviceDto model, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(model.UserName))
        {
            return ApiResult.Fail("Username is required");
        }
        var userInfo = await _iAuthRepo.GetUserInfoByUserNameAsync(model.UserName, cancellationToken);
        if (userInfo is null || userInfo.Id == Guid.Empty)
        {
            return ApiResult.Fail("User not found");
        }
        //first delete all record by userId
        var authTokenList = await _dbContext.AuthTokens.Where(x => x.UserName == model.UserName).ToListAsync(cancellationToken);
        if (authTokenList != null && authTokenList.Count > 0)
        {
            _dbContext.AuthTokens.RemoveRange(authTokenList);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        //Then add this record
        await _dbContext.AuthTokens.AddAsync(model.DtoToEntity());
        await _dbContext.SaveChangesAsync(cancellationToken);
        return ApiResult.Success();
    }
}
