using Application.Common.Features.UserManage.Users.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.UserModule;

[ApiController]
[Route("api/[controller]")]
public class UserController : BaseController
{
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok((await Mediator.Send(new GetUserByIdQuery { IdUser = id })).Data);
    }
}