using MediatR;
using CommonCore.InterfaceAdapters.Dtos;
using CommonCore.Core.Interfaces;

namespace CommonCore.Application.Commands.AuthToken;

public class LogoutCommandHandler(IAuthTokenRepository _iAuthTokenRepo) : IRequestHandler<LogoutCommand, ApiResult>
{
    public async Task<ApiResult> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        return await _iAuthTokenRepo.DeleteAuthToken(request.userId, cancellationToken);
    }
}
