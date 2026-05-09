using MediatR;
using CommonCore.InterfaceAdapters.Dtos;

namespace CommonCore.Application.Commands.AuthToken;

public record SaveRememberMeCommand(string userId, string code, bool isRememberMe, CancellationToken cancellationToken) : IRequest<ApiResult>;
