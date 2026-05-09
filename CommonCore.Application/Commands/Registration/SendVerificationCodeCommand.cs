using MediatR;
using CommonCore.InterfaceAdapters.Dtos;

namespace CommonCore.Application.Commands.AuthToken;

public record SendVerificationCodeCommand(string sendCodeOn, string sendCodeVia, string userId, CancellationToken cancellationToken) : IRequest<ApiResult>;
