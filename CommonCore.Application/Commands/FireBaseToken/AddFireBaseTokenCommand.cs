using CommonCore.InterfaceAdapters.Dtos;
using CommonCore.InterfaceAdapters.Dtos.Auth.FireBase;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonCore.Application.Commands.FireBaseToken;

public record AddFireBaseTokenCommand(FirebaseTokenVM model, CancellationToken cancellationToken) : IRequest<ApiResult>;
