using Application.Common.Features.UserManage.Permissions.Queries;
using Application.Common.Interfaces;
using Application.Wrappers;
using Domain.Entities.UserModule;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace Application.Common.Features.UserManage.Roles.Queries;

public sealed class GetRoleByIdQuery : IQuery<Response<Role>>
{
    public short IdRole { get; set; }
    [DefaultValue(false)]
    public bool RoleEnabled { get; set; }
}

public sealed class GetRoleByIdQueryHandler : IQueryHandler<GetRoleByIdQuery, Response<Role>>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IMediator _mediator;

    public GetRoleByIdQueryHandler(IApplicationDbContext applicationDbContext, IMediator mediator)
    {
        _applicationDbContext = applicationDbContext;
        _mediator = mediator;
    }

    public async Task<Response<Role>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        Role role = await this._applicationDbContext.dbContext.Set<Role>().FirstOrDefaultAsync(r => r.Id == request.IdRole) ?? new Role();
        // si exige que se devuelvan roles habilitados y que este no lo sea, entra a la condicion
        if (request.RoleEnabled && role is Role { Id: > 0, isEnabled: false })
            role = new Role();
        else
            role.Permissions = (await this._mediator.Send(new GetPermissionsByIdRoleQuery { IdRole = role.Id })).Data;
        return new Response<Role>(role);
    }
}
