using CommonCore.Core.Entities;
using CommonCore.InterfaceAdapters.Dtos;
using CommonCore.InterfaceAdapters.Dtos.Auth;

namespace CommonCore.Core.Interfaces;

public interface IAuthRepository
{
    Task<bool> IsPhoneNumberExist(string phoneNumber);
    Task<AspNetUser> GetUserInfoByUserNameAsync(string userName,CancellationToken cancellationToken);
    Task<ApiResult> ChangePassword(ChangePassword model,CancellationToken cancellationToken);
    Task<ApiResult> UpdateUserInfo(ProfileUserDto model);
    Task<bool> IsUserPhoneNumberExist(string phoneNumber,Guid userId);
    Task<bool> IsUserEmailExist(string email,Guid userId);
    Task<ApiResult> SaveRememberMe(string userId, string code, bool isRememberMe, CancellationToken cancellationToken);
}
