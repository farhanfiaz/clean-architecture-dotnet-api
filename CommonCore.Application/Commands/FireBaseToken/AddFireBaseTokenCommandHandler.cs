using CommonCore.Core.Interfaces;
using CommonCore.InterfaceAdapters.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonCore.Application.Commands.FireBaseToken;

public class AddFireBaseTokenCommandHandler(IFireBaseRepository _iFireBaseRepo) : IRequestHandler<AddFireBaseTokenCommand, ApiResult>
{
    public async Task<ApiResult> Handle(AddFireBaseTokenCommand request, CancellationToken cancellationToken)
    {
        return await _iFireBaseRepo.AddFireBaseToken(request.model, cancellationToken);
    }
}
