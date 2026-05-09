using MediatR;
using CommonCore.InterfaceAdapters.Dtos;
using CommonCore.Core.Interfaces;

namespace CommonCore.Application.Commands.AuthToken;

public class SaveRememberMeCommandHandler(IAuthRepository _iAuthRepo) : IRequestHandler<SaveRememberMeCommand, ApiResult>
{
    public async Task<ApiResult> Handle(SaveRememberMeCommand request, CancellationToken cancellationToken)
    {
        return await _iAuthRepo.SaveRememberMe(request.userId, request.code, request.isRememberMe, cancellationToken);
    }
}
