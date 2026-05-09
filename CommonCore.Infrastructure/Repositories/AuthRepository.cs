using CommonCore.Core.Entities;
using CommonCore.Core.Interfaces;
using CommonCore.Infrastructure.Data;
using CommonCore.InterfaceAdapters.Dtos;
using CommonCore.InterfaceAdapters.Dtos.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CommonCore.Infrastructure.Repositories;

public class AuthRepository(CommonCoreDbContext dbContext, UserManager<AspNetUser> _userManager)
 : IAuthRepository
{
    public async Task<ApiResult> ChangePassword(ChangePassword model, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(model.UserId))
        {
            return ApiResult.Fail("UserId is required");
        }
        var user = await _userManager.FindByIdAsync(model.UserId);
        if (user is null)
        {
            return ApiResult.Fail("User not found");
        }
        var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        if (!result.Succeeded)
        {
            return ApiResult.Fail("User cannot be updated");
        }
        return ApiResult.Success();
    }

    public async Task<AspNetUser> GetUserInfoByUserNameAsync(string userName, CancellationToken cancellationToken)
    {
        return await dbContext.AspNetUsers.FirstOrDefaultAsync(x => x.UserName == userName, cancellationToken) ?? new AspNetUser();
    }

    public async Task<bool> IsPhoneNumberExist(string phoneNumber)
    {
        return await dbContext.AspNetUsers.AnyAsync(x => x.PhoneNumber == phoneNumber);
    }

    public async Task<bool> IsUserEmailExist(string email, Guid userId)
    {
        return await dbContext.AspNetUsers.AnyAsync(x => x.Email == email && x.Id != userId);
    }

    public async Task<bool> IsUserPhoneNumberExist(string phoneNumber, Guid userId)
    {
        return await dbContext.AspNetUsers.AnyAsync(x => x.PhoneNumber == phoneNumber && x.Id != userId);
    }

    public async Task<ApiResult> SaveRememberMe(string userId, string code, bool isRememberMe, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return ApiResult.Fail("UserId is required");
        }
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            return ApiResult.Fail("User not found");
        }
        if (!string.IsNullOrEmpty(user.TwoFactorCode) && code == user.TwoFactorCode)
        {
            if (isRememberMe)
            {
                user.RememberMe = DateTime.UtcNow.AddDays(30);
            }
            user.TwoFactorCode = null;
            await _userManager.UpdateAsync(user);
            return ApiResult.Success();
        }
        return ApiResult.Fail("User not found");
    }

    public async Task<ApiResult> UpdateUserInfo(ProfileUserDto model)
    {
        if (string.IsNullOrWhiteSpace(model.UserId))
        {
            return ApiResult.Fail("UserId is required");
        }
        var user = await _userManager.FindByIdAsync(model.UserId);
        if (user is null)
        {
            return ApiResult.Fail("User not found");
        }

        if (await IsUserEmailExist(model.Email, user.Id))
        {
            return ApiResult.Fail("Email already exist");
        }

        if (await IsUserPhoneNumberExist(model.PhoneNumber, user.Id))
        {
            return ApiResult.Fail("Phone number already exist");
        }

        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.PhoneNumber = model.PhoneNumber;
        user.Email = model.Email;
        user.DateOfBirth = model.DateOfBirth;
        user.FullName = model.FirstName + " " + model.LastName;
        user.IsTwoFactorEnabled = model.IsTwoFactorEnabled;

        await _userManager.UpdateAsync(user);
        return ApiResult.Success();
    }
}
