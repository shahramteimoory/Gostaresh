using MachineReporting.Api.Dtos;
using MachineReporting.Api.Jwt;
using MachineReporting.Api.Services.UsersServices.Commands;
using MachineReporting.Api.Services.UsersServices.Commands.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MachineReporting.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController(IUserManagmentServices userManagment,CreateJwtTokenAsynco createJwtToken) : ControllerBase
    {
        [Route("Login")]
        [HttpPost]
        public async Task<ActionResult<ReturnToken>> Login([FromForm] TokenRequest request)
        {
            var user = await userManagment.Login(request);
            if (user.IsSuccess)
                return Ok(createJwtToken.ExecuteAsync(user.Data));
            
            return BadRequest(user);
        }

        [HttpPut]
        public async Task<ActionResult<ReturnToken>> ChangePassword(ChangePasswordDto request)
        {
            return Ok(await userManagment.ChangePassword(request));
        }
    }
}