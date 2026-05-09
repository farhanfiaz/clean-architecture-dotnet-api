using MediatR;
using Microsoft.AspNetCore.Identity;
using CommonCore.InterfaceAdapters.Dtos;
using CommonCore.Core.Entities;
using CommonCore.InterfaceAdapters.Dtos.Auth;
using CommonCore.Core.Interfaces;
using CommonCore.Application.Events.User;
using CommonCore.Core.DtoExtensions;

namespace CommonCore.Application.Commands.Registration;

public class RegisterUserCommandHandler(IAuthRepository _iAuthRepo
    , UserManager<AspNetUser> _userManager, SignInManager<AspNetUser> _signInManager
    , RoleManager<AspNetRole> _roleManager, IPasswordHasher<AspNetUser> _passwordHasher
    , IJwtService _iJWTTokenService, IMediator _iMediator) : IRequestHandler<RegisterUserCommand, ApiResult<AuthResponceVM>>
{
    public async Task<ApiResult<AuthResponceVM>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var responce = new AuthResponceVM();
        if (string.IsNullOrWhiteSpace(request.model.Email))
        {
            return ApiResult<AuthResponceVM>.Fail("Email required");
        }
        var isEmailExist = await _userManager.FindByEmailAsync(request.model.Email);
        if (isEmailExist != null)
        {
            return ApiResult<AuthResponceVM>.Fail("Email already exist");
        }
        if (await _iAuthRepo.IsPhoneNumberExist(request.model.PhoneNumber))
        {
            return ApiResult<AuthResponceVM>.Fail("Phone number already exist");
        }
        if (string.IsNullOrWhiteSpace(request.model.UserType))
        {
            return ApiResult<AuthResponceVM>.Fail("User role required");
        }
        if (!await _roleManager.RoleExistsAsync(request.model.UserType))
        {
            return ApiResult<AuthResponceVM>.Fail("InValid User Role");
        }
        var user = request.model.DtoToEntity();
        user.PasswordHash = _passwordHasher.HashPassword(user, request.model.Password);
        var result = await _userManager.CreateAsync(user);
        if (!result.Succeeded)
        {
            return ApiResult<AuthResponceVM>.Fail(result.Errors.Select(x => x.Description).ToList().ToString());
        }
        await _userManager.AddToRoleAsync(user, request.model.UserType);

        await _iMediator.Publish(new UserCreatedEvent(user));

        return ApiResult<AuthResponceVM>.Success(user.GenerateAuthResponce(_iJWTTokenService.GenerateToken(user)));
    }
}
