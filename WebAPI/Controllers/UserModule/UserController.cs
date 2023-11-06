using Application.Common.Features.UserManage.Users.Command;
using Application.Common.Features.UserManage.Users.Queries;
using Application.Wrappers;
using Domain.Entities.UserModule;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.UserModule;

[ApiController]
[Route("api/[controller]")]
public class UserController : BaseController
{
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            Response<User> response = await Mediator.Send(new GetUserByIdQuery { IdUser = id });
            return Ok(response.Data);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(User user)
    {
        try
        {
            Response<User> response = await Mediator.Send(new AddUserCommand { User = user });
            return Ok(response.Data);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}