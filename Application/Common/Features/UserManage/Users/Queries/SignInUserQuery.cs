using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Wrappers;
using Domain.Entities.UserModule;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Features.UserManage.Users.Queries;

public sealed class SignInUserQuery : IQuery<Response<User>>
{
    public User User { get; set; }
}

public sealed class SignInUserQueryHandler : IQueryHandler<SignInUserQuery, Response<User>>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public SignInUserQueryHandler(IApplicationDbContext applicationDbContext)
    {
        this._applicationDbContext = applicationDbContext;
    }

    public async Task<Response<User>> Handle(SignInUserQuery request, CancellationToken cancellationToken)
    {
        User user = await this._applicationDbContext.dbContext.Set<User>().FirstOrDefaultAsync(u => u.Email == request.User.Email || u.Nickname == request.User.Nickname);
        if (user is null || !user.isEnabled)
            throw new Exception($"No se encontro el usuario {(string.IsNullOrEmpty(request.User.Nickname) ? request.User.Email : request.User.Nickname)}");
        if (!user.VerifyPassword(request.User.password))
            throw new Exception("Contrasenia incorrecta");
        return new Response<User>(user);
    }
}
