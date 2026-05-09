using CommonCore.Core.Interfaces;
using CommonCore.InterfaceAdapters.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonCore.Application.Commands.AuthToken;

public class AddAuthTokenCommandHandler(IAuthTokenRepository _iAuthTokenRepo) : IRequestHandler<AddAuthTokenCommand, ApiResult>
{
    public async Task<ApiResult> Handle(AddAuthTokenCommand request, CancellationToken cancellationToken)
    {
        return await _iAuthTokenRepo.SaveAuthToken(request.model, cancellationToken);
    }
}
