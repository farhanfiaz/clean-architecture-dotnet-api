using MediatR;
using CommonCore.InterfaceAdapters.Dtos;
using CommonCore.InterfaceAdapters.Dtos.Auth;

namespace CommonCore.Application.Commands.AuthToken;

public record UpdatePatientProfileDetailCommand(ProfileUserDto model, CancellationToken cancellationToken) : IRequest<ApiResult>;
