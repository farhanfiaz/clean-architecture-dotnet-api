using MediatR;
using CommonCore.InterfaceAdapters.Dtos;
using CommonCore.Core.Interfaces;

namespace CommonCore.Application.Commands.AuthToken;

public class ChangePatientPasswordCommandHandler(IAuthRepository _iAuthRepo) : IRequestHandler<ChangePatientPasswordCommand, ApiResult>
{
    public async Task<ApiResult> Handle(ChangePatientPasswordCommand request, CancellationToken cancellationToken)
    {
        return await _iAuthRepo.ChangePassword(request.model, cancellationToken);
    }
}
