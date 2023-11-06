using Application.Common.Interfaces;
using Application.Wrappers;
using Domain.Entities.UserModule;
using Domain.Interfaces;

namespace Application.Common.Features.UserManage.Roles.Command;

public sealed class AddRoleCommand : IQuery<Response<Role>>
{
    public Role Role { get; set; }
}

public sealed class AddRoleCommandHandler : IQueryHandler<AddRoleCommand, Response<Role>>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public AddRoleCommandHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Response<Role>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
    {
        await _applicationDbContext.dbContext.Set<Role>().AddAsync(request.Role);
        await this._applicationDbContext.dbContext.SaveChangesAsync(cancellationToken);
        return new Response<Role> { Data = request.Role };
    }
}