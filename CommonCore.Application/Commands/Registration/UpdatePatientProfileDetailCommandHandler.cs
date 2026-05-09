using CommonCore.Core.Interfaces;
using CommonCore.InterfaceAdapters.Dtos;
using MediatR;

namespace CommonCore.Application.Commands.AuthToken;

public class UpdatePatientProfileDetailCommandHandler(IAuthRepository _iAuthRepo) : IRequestHandler<UpdatePatientProfileDetailCommand, ApiResult>
{
    public async Task<ApiResult> Handle(UpdatePatientProfileDetailCommand request, CancellationToken cancellationToken)
    {
        return await _iAuthRepo.UpdateUserInfo(request.model);
    }
}