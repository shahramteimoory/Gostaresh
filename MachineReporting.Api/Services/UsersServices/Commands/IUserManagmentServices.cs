using MachineReporting.Api.Dtos;
using MachineReporting.Api.Models.Entities;
using MachineReporting.Api.Services.UsersServices.Commands.Dto;

namespace MachineReporting.Api.Services.UsersServices.Commands
{
    public interface IUserManagmentServices
    {
        Task<ApiResult<string>> AddUser(AddUserDto request);
        Task<ApiResult> UpdateUser(EdutUserDto edutUserDto);
        Task<ApiResult> DeleteUser(int id);
        Task<ApiResult> ChangePassword(ChangePasswordDto request);
        Task<ApiResult<Users>> Login(TokenRequest request);
        Task<ApiResult> AddBalance(long accountNumber,decimal amount);
    }
}