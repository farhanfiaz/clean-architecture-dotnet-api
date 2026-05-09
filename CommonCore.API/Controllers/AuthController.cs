using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CommonCore.Application.Commands.AuthToken;
using CommonCore.Application.Commands.FireBaseToken;
using CommonCore.Application.Commands.Registration;
using CommonCore.Application.Queries.Login;
using CommonCore.Core.Entities;
using CommonCore.InterfaceAdapters.Dtos;
using CommonCore.InterfaceAdapters.Dtos.Auth;
using CommonCore.InterfaceAdapters.Dtos.Auth.FireBase;

namespace CommonCore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AuthController(IMediator _iMediator) : ControllerBase
{
    /// <summary>
    /// User Login 
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("LoginAsync")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResult<AuthResponceVM>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResult<AuthResponceVM>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResult<AuthResponceVM>))]
    public async Task<ApiResult<AuthResponceVM>> LoginAsync([FromBody] LoginDto model, CancellationToken cancellationToken)
    {
        return await _iMediator.Send(new LoginUserRequest(model));
    }
    /// <summary>
    /// SignUp
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("RegisterAsync")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResult<AuthResponceVM>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResult<AuthResponceVM>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResult<AuthResponceVM>))]
    public async Task<ApiResult<AuthResponceVM>> RegisterAsync([FromBody] RegisterDto model, CancellationToken cancellationToken)
    {
        return await _iMediator.Send(new RegisterUserCommand(model));
    }
    /// <summary>
    ///  Create New Role
    /// </summary>
    /// <param name="roleName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("AddRoleAsync")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResult<string>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResult<string>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResult<string>))]
    public async Task<ApiResult<string>> AddRoleAsync(string roleName, CancellationToken cancellationToken)
    {
        return await _iMediator.Send(new AddRoleCommand(roleName, cancellationToken));
    }
    /// <summary>
    /// Get Role List
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("GetRoleListAsync")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AspNetRole>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(List<AspNetRole>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(List<AspNetRole>))]
    public async Task<List<AspNetRole>> GetRoleListAsync(CancellationToken cancellationToken)
    {
        return await _iMediator.Send(new GetRoleRequest(cancellationToken));
    }
    /// <summary>
    /// Get Refresh Token by email
    /// </summary>
    /// <param name="email"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("RefreshTokenAsync")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResult<string>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResult<string>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResult<string>))]
    public async Task<ApiResult<string>> RefreshTokenAsync(string email, CancellationToken cancellationToken)
    {
        return await _iMediator.Send(new RefreshTokenRequest(email, cancellationToken));
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("SingleDeviceLoggedIn")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ApiResult> SingleDeviceLoggedIn(SingleDeviceDto model, CancellationToken cancellationToken)
    {
        return await _iMediator.Send(new AddAuthTokenCommand(model, cancellationToken));
    }
    /// <summary>
    /// Logout
    /// </summary>
    /// <param name="UserId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("Logout")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ApiResult> Logout(string UserId, CancellationToken cancellationToken)
    {
        return await _iMediator.Send(new LogoutCommand(UserId, cancellationToken));
    }
    /// <summary>
    /// SaveFirebaseToken
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("SaveFirebaseToken")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ApiResult> SaveFirebaseToken(FirebaseTokenVM model, CancellationToken cancellationToken)
    {
        return await _iMediator.Send(new AddFireBaseTokenCommand(model, cancellationToken));
    }
    [HttpGet("SendVerificationCode")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ApiResult> SendVerificationCode(string sendCodeOn, string sendCodeVia, string userId, CancellationToken cancellationToken)
    {
        return await _iMediator.Send(new SendVerificationCodeCommand(sendCodeOn, sendCodeVia, userId, cancellationToken));
    }

    [HttpGet("SaveRememberMe")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ApiResult> SaveRememberMe(string userId, string code, bool isRememberMe, CancellationToken cancellationToken)
    {
        return await _iMediator.Send(new SaveRememberMeCommand(userId, code, isRememberMe, cancellationToken));
    }
    [HttpGet("ChangePatientPassword")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ApiResult> ChangePatientPassword(ChangePassword model, CancellationToken cancellationToken)
    {
        return await _iMediator.Send(new ChangePatientPasswordCommand(model, cancellationToken));
    }
    [HttpGet("UpdatePatientProfileDetail")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ApiResult> UpdatePatientProfileDetail(ProfileUserDto model, CancellationToken cancellationToken)
    {
        return await _iMediator.Send(new UpdatePatientProfileDetailCommand(model, cancellationToken));
    }
}
