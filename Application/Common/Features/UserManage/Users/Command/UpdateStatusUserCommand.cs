using Application.Common.Features.UserManage.Users.Queries;
using Application.Common.Interfaces;
using Application.Wrappers;
using Domain.Entities.UserModule;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Features.UserManage.Users.Command;

public sealed class UpdateStatusUserCommand : IQuery<Response<bool>>
{
    public int IdUser { get; set; }
    public bool NewStatus { get; set; }
}

public sealed class UpdateStatusUserCommandHandler : IQueryHandler<UpdateStatusUserCommand, Response<bool>>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IMediator _mediator;

    public UpdateStatusUserCommandHandler(IApplicationDbContext applicationDbContext, IMediator mediator)
    {
        _applicationDbContext = applicationDbContext;
        _mediator = mediator;
    }

    public async Task<Response<bool>> Handle(UpdateStatusUserCommand request, CancellationToken cancellationToken)
    {
        Response<User> response = await _mediator.Send(new GetUserByIdQuery { IdUser = request.IdUser });
        User user = response.Data;
        if (user is null)
            throw new Exception("usuario no existe");
        user.isEnabled = request.NewStatus;
        await _applicationDbContext.dbContext.SaveChangesAsync(cancellationToken);
        return new Response<bool>(true);
    }
}
