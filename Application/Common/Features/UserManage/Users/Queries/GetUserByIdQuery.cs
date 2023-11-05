using Application.Common.Interfaces;
using Application.Wrappers;
using Domain.Entities.UserModule;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Features.UserManage.Users.Queries;

public sealed class GetUserByIdQuery : IQuery<Response<User>>
{
    public int IdUser { get; set; }
}

public sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, Response<User>>
{
    public readonly IApplicationDbContext applicationDbContext;

    public GetUserByIdQueryHandler(IApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }

    public async Task<Response<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        User user = await this.applicationDbContext.dbContext.Set<User>().FirstOrDefaultAsync(u => u.Id == request.IdUser) ?? new User();
        return new Response<User>(user);
    }
}
