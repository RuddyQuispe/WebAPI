using Application.Common.Interfaces;
using Application.Wrappers;
using Domain.Entities.UserModule;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Features.UserManage.Users.Command;

public sealed class UpdateStatusUserCommand:IQuery<Response<bool>>
{
    public int IdUser { get; set; }
    public bool NewStatus { get; set; }
}

public sealed class UpdateStatusUserCommandHandler : IQueryHandler<UpdateStatusUserCommand, Response<bool>>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public UpdateStatusUserCommandHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Response<bool>> Handle(UpdateStatusUserCommand request, CancellationToken cancellationToken)
    {
        User user = await _applicationDbContext.dbContext.Set<User>().FirstOrDefaultAsync(u => u.Id == request.IdUser);
        if (user is null)
            throw new Exception("usuario no existe");
        user.isEnabled = request.NewStatus;
        await _applicationDbContext.dbContext.SaveChangesAsync(cancellationToken);
        return new Response<bool>(true);
    }
}
