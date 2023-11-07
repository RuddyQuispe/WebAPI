using Application.Common.Features.UserManage.Roles.Queries;
using Application.Common.Features.UserManage.Users.Queries;
using Application.Wrappers;
using Domain.Entities.UserModule;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.UserModule;

[ApiController]
[Route("api/[controller]")]
public class RoleController : BaseController
{
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(short id)
    {
        try
        {
            Response<Role> response = await Mediator.Send(new GetRoleByIdQuery { IdRole = id });
            return Ok(response.Data);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
