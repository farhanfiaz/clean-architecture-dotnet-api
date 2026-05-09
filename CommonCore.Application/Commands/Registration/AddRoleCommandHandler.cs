using MediatR;
using Microsoft.AspNetCore.Identity;
using CommonCore.InterfaceAdapters.Dtos;
using CommonCore.Core.Entities;

namespace CommonCore.Application.Commands.Registration;

public class AddRoleCommandHandler(RoleManager<AspNetRole> _roleManager) : IRequestHandler<AddRoleCommand, ApiResult<string>>
{
    public async Task<ApiResult<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
    {
        if (!await _roleManager.RoleExistsAsync(request.roleName))
        {
            await _roleManager.CreateAsync(new AspNetRole
            {
                Name = request.roleName
            });
            return ApiResult<string>.Success("Successfully added");
        }
        return ApiResult<string>.Fail("role already exist");
    }
}
