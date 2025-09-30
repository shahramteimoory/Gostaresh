using System.Threading.Tasks;
using MachineReporting.Api.Dtos;
using MachineReporting.Api.Models.Entities;
using MachineReporting.Api.Services.Facades;
using MachineReporting.Api.Services.UsersServices.Commands;
using MachineReporting.Api.Services.UsersServices.Commands.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MachineReporting.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController(IUserFacade userManagmentServices) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<ApiResult>> Add(AddUserDto request)
        {
            return Ok(await userManagmentServices.UserManagmentServices.AddUser(request));
        }

        [HttpPut]
        public async Task<ActionResult<ApiResult>> Edit(EdutUserDto request)
        {
            return Ok(await userManagmentServices.UserManagmentServices.UpdateUser(request));
        }

        [HttpDelete]
        public async Task<ActionResult<ApiResult>> Delete(int id)
        {
            return Ok(await userManagmentServices.UserManagmentServices.DeleteUser(id));
        }

        [HttpPost("AddBalance")]
        public async Task<ActionResult> AddBalance(long accountNumber, decimal balance)
        {
            return Ok(await userManagmentServices.UserManagmentServices.AddBalance(accountNumber, balance));
        }

        [HttpGet("{accountNumber}")]
        public async Task<ActionResult> GetAccountWithAccountNumber(long accountNumber)
        {
            return Ok(await userManagmentServices.GetUserManagmentServices.Get(accountNumber));
        }

        [HttpGet("id")]
        public async Task<ActionResult> GetAccount(int id)
        {
            return Ok(await userManagmentServices.GetUserManagmentServices.Get(id));
        }

        [HttpGet("GetByUsersType")]
        public async Task<ActionResult> GetUsersByType(UserType userType)
        {
            return Ok(await userManagmentServices.GetUserManagmentServices.GetType(userType));
        }
    }
}