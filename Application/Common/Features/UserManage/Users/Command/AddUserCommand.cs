using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Wrappers;
using Domain.Entities.UserModule;
using Domain.Interfaces;

namespace Application.Common.Features.UserManage.Users.Command;

public sealed class AddUserCommand : ICommand<Response<User>>
{
    public User User { get; set; }
}

public sealed class AddUserCommandHandler : ICommandHandler<AddUserCommand, Response<User>>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public AddUserCommandHandler(IApplicationDbContext applicationDbContext)
    {
        this._applicationDbContext = applicationDbContext;
    }

    public async Task<Response<User>> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        request.User.HashPasword();
        await this._applicationDbContext.dbContext.Set<User>().AddAsync(request.User);
        await this._applicationDbContext.dbContext.SaveChangesAsync(cancellationToken);
        // clean password and hash info
        request.User.password = request.User.PasswordHash = request.User.PasswordSalt = "";
        return new Response<User> { Data = request.User };
    }
}
