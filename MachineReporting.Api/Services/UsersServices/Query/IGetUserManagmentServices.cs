using MachineReporting.Api.Dtos;
using MachineReporting.Api.Models.Entities;
using MachineReporting.Api.Services.Query.Dto;

namespace MachineReporting.Api.Services.Query
{
    public interface IGetUserManagmentServices
    {
        Task<ApiResult<UserDto>> Get(long AccountNumber);
        Task<ApiResult<UserDto>> Get(int id);
        Task<ApiResult<List<UserDto>>> GetType(UserType userType);
    }
}