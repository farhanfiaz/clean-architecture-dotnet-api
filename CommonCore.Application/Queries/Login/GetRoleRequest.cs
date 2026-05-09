using CommonCore.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace CommonCore.Application.Queries.Login;

public record GetRoleRequest(CancellationToken cancellationToken) : IRequest<List<AspNetRole>>;

public class GetRoleHandler(RoleManager<AspNetRole> _roleManager)
    : IRequestHandler<GetRoleRequest, List<AspNetRole>>
{
    public async Task<List<AspNetRole>> Handle(GetRoleRequest request, CancellationToken cancellationToken)
    {
        return await _roleManager.Roles.ToListAsync(cancellationToken);
    }
}
