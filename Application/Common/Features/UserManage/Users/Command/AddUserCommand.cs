using Application.Common.Interfaces;
using Application.Wrappers;
using Domain.Entities.UserModule;
using Domain.Interfaces;

namespace Application.Common.Features.UserManage.Users.Command;

public sealed class AddUserCommand:ICommand<Response<int>>
{
    public User User { get; set; }
}

public sealed class AddUserCommandHandler : ICommandHandler<AddUserCommand, Response<int>>
{
    private readonly IApplicationDbContext applicationDbContext;

    public AddUserCommandHandler(IApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }

    public async Task<Response<int>> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        this.applicationDbContext.dbContext.Set<User>().Add(request.User);
        await this.applicationDbContext.dbContext.SaveChangesAsync(cancellationToken);
        return new Response<int>(0);
    }
}
