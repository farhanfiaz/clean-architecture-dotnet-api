using CommonCore.Core.DtoExtensions;
using CommonCore.Core.Entities;
using CommonCore.Core.Interfaces;
using CommonCore.InterfaceAdapters.Dtos;
using CommonCore.InterfaceAdapters.Dtos.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CommonCore.Application.Queries.Login;

public class LoginUserHandler(IAuthRepository authRepo, UserManager<AspNetUser> _userManager
    , SignInManager<AspNetUser> _signInManager, RoleManager<AspNetRole> _roleManager
    , IJwtService _iJWTTokenService)
    : IRequestHandler<LoginUserRequest, ApiResult<AuthResponceVM>>
{
    public async Task<ApiResult<AuthResponceVM>> Handle(LoginUserRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.model.Email))
        {
            return ApiResult<AuthResponceVM>.Fail("Email required");
        }
        if (string.IsNullOrWhiteSpace(request.model.Password))
        {
            return ApiResult<AuthResponceVM>.Fail("Password required");
        }
        var isUserExist = await _userManager.FindByEmailAsync(request.model.Email);
        if (isUserExist is null)
        {
            return ApiResult<AuthResponceVM>.Fail("Email not found");
        }
        var result = await _signInManager.PasswordSignInAsync(isUserExist, request.model.Password, request.model.IsRememberMe, false);
        if (!result.Succeeded)
        {
            return ApiResult<AuthResponceVM>.Fail("InValid password");
        }
        return ApiResult<AuthResponceVM>.Success(isUserExist.GenerateAuthResponce(_iJWTTokenService.GenerateToken(isUserExist)));
    }
    
}
