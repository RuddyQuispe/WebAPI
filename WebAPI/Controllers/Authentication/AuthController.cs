using Application.Common.Features.UserManage.Users.Queries;
using Application.Wrappers;
using Domain.Entities.UserModule;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Authentication;

[ApiController]
[Route("api/[controller]")]
public class AuthController:BaseController
{
    [HttpPost]
    public async Task<IActionResult> Post(User user)
    {
        try
        {
            Response<User> response = await Mediator.Send(new SignInUserQuery { User = user });
            return Ok(response.Data);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
