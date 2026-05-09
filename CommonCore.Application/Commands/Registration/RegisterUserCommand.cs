using MediatR;
using CommonCore.InterfaceAdapters.Dtos;
using CommonCore.InterfaceAdapters.Dtos.Auth;

namespace CommonCore.Application.Commands.Registration;

public record RegisterUserCommand(RegisterDto model) : IRequest<ApiResult<AuthResponceVM>>;
