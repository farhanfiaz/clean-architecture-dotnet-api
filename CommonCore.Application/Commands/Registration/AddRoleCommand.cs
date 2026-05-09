using MediatR;
using CommonCore.InterfaceAdapters.Dtos;

namespace CommonCore.Application.Commands.Registration;

public record AddRoleCommand(string roleName, CancellationToken CancellationToken) : IRequest<ApiResult<string>>;
