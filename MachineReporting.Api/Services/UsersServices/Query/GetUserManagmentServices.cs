using System.Net;
using MachineReporting.Api.Dtos;
using MachineReporting.Api.Models.DataBaseContext;
using MachineReporting.Api.Models.Entities;
using MachineReporting.Api.Services.Query.Dto;
using Microsoft.EntityFrameworkCore;

namespace MachineReporting.Api.Services.Query
{
    public class GetUserManagmentServices(DataBaseContext context) : IGetUserManagmentServices
    {
        public async Task<ApiResult<UserDto>> Get(long accountNumber)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.AccountNumber == accountNumber.ToString());
            if (user is null)
                return new ApiResult<UserDto> { StatusCode = HttpStatusCode.NotFound, Message = "موردی یافت نشد" };

            return new ApiResult<UserDto>
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Data=new UserDto
                {
                    AccountNumber = user.AccountNumber,
                    ClientId = user.ClientId,
                    Id = user.Id,
                    Age = user.Age,
                    userType = user.userType,
                    Balance=user.Balance  
                }
            };
        }

        public async Task<ApiResult<UserDto>> Get(int id)
        {
            var user = await context.Users.FindAsync(id);
            if (user is null)
                return new ApiResult<UserDto> { StatusCode = HttpStatusCode.OK, Message = "موردی یافت نشد" };

            return new ApiResult<UserDto>
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Data = new UserDto
                {
                    AccountNumber = user.AccountNumber,
                    ClientId = user.ClientId,
                    Id = user.Id,
                    Age = user.Age,
                    userType = user.userType,
                    Balance = user.Balance
                }
            };
        }

        public async Task<ApiResult<List<UserDto>>> GetType(UserType userType)
        {
            var result = await context.Users.Where(x => x.userType == userType).
            Select(s => new UserDto
            {
                AccountNumber = s.AccountNumber,
                ClientId = s.ClientId,
                Id = s.Id,
                Age = s.Age,
                userType = s.userType,
                Balance = s.Balance
            }).ToListAsync();

            return new ApiResult<List<UserDto>>
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Data = result
            };
        }
    }
}