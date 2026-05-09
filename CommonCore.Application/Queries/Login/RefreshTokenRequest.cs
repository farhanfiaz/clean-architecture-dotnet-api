using System;
using CommonCore.Core.Entities;
using CommonCore.Core.Interfaces;
using CommonCore.InterfaceAdapters.Dtos;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CommonCore.Application.Queries.Login;

public record RefreshTokenRequest(string email, CancellationToken cancellationToken) : IRequest<ApiResult<string>>;

public class RefreshTokenHandler(UserManager<AspNetUser> _userManager, IJwtService _iJwtService)
 : IRequestHandler<RefreshTokenRequest, ApiResult<string>>
{
    public async Task<ApiResult<string>> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.email))
        {
            return ApiResult<string>.Fail("Email missing.");
        }
        var user = await _userManager.FindByEmailAsync(request.email);
        if (user == null)
        {
            return ApiResult<string>.Fail("User info not found.");
        }
        return ApiResult<string>.Success(_iJwtService.GenerateToken(user));
    }
}
