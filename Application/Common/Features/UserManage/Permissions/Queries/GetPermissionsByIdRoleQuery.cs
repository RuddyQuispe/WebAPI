using Application.Common.Interfaces;
using Application.Wrappers;
using Domain.Entities.UserModule;
using Domain.Interfaces;
using System.ComponentModel;

namespace Application.Common.Features.UserManage.Permissions.Queries;

public sealed class GetPermissionsByIdRoleQuery : IQuery<Response<ICollection<Permission>>>
{
    public short IdRole { get; set; }
    [DefaultValue(false)]
    public bool RoleEnabled { get; set; }
}

public sealed class GetPermissionsByIdRoleQueryHandler : IQueryHandler<GetPermissionsByIdRoleQuery, Response<ICollection<Permission>>>
{
    public readonly IApplicationDbContext _applicationDbContext;

    public GetPermissionsByIdRoleQueryHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Response<ICollection<Permission>>> Handle(GetPermissionsByIdRoleQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Permission> permissions = from role in this._applicationDbContext.dbContext.Set<Role>()
                                             join rolePermission in this._applicationDbContext.dbContext.Set<RolePermission>()
                                                on role.Id equals rolePermission.IdRole
                                             join permission in this._applicationDbContext.dbContext.Set<Permission>()
                                                on rolePermission.IdPermission equals permission.Id
                                             where rolePermission.IdRole == request.IdRole && (request.RoleEnabled ? role.isEnabled : true)
                                             select permission;
        ICollection<Permission> permissionsList = permissions.ToList();
        return new Response<ICollection<Permission>>(permissionsList);
    }
}
