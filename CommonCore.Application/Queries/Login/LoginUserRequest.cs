using CommonCore.InterfaceAdapters.Dtos;
using CommonCore.InterfaceAdapters.Dtos.Auth;
using MediatR;

namespace CommonCore.Application.Queries.Login;

public record LoginUserRequest(LoginDto model) : IRequest<ApiResult<AuthResponceVM>>;
