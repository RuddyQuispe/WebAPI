using Application.Common.Features.UserManage.Roles.Queries;
using Application.Common.Interfaces;
using Application.Wrappers;
using Domain.Entities.UserModule;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace Application.Common.Features.UserManage.Users.Queries;

public sealed class GetUserByIdQuery : IQuery<Response<User>>
{
    public int IdUser { get; set; }
    [DefaultValue(false)]
    public bool UserEnabled { get; set; }
}

public sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, Response<User>>
{
    public readonly IApplicationDbContext _applicationDbContext;
    public readonly IMediator _mediator;

    public GetUserByIdQueryHandler(IApplicationDbContext applicationDbContext, IMediator mediator)
    {
        _applicationDbContext = applicationDbContext;
        _mediator = mediator;
    }

    public async Task<Response<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        User user = await this._applicationDbContext.dbContext.Set<User>().FirstOrDefaultAsync(u => u.Id == request.IdUser) ?? new User();
        // si exige que se devuelvan usuarios habilitados y que este no lo sea, entra a la condicion
        if (request.UserEnabled && user is User { Id: > 0, isEnabled: false })
            user = new User();
        if (user is User { Id: > 0 })
            user.Role = (await this._mediator.Send(new GetRoleByIdQuery { IdRole = user.IdRole, RoleEnabled = true })).Data;
        return new Response<User>(user);
    }
}
