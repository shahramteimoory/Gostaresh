using System.Net;
using MachineReporting.Api.Dtos;
using MachineReporting.Api.Models.DataBaseContext;
using MachineReporting.Api.Models.Entities;
using MachineReporting.Api.Services.UsersServices.Commands.Dto;
using MachineReporting.Api.Utilities;
using Microsoft.EntityFrameworkCore;

namespace MachineReporting.Api.Services.UsersServices.Commands
{
    public class UserManagmentServices(DataBaseContext context) : IUserManagmentServices
    {
        public async Task<ApiResult> AddBalance(long accountNumber, decimal amount)
        {

            var user = await context.Users.FirstOrDefaultAsync(x => x.AccountNumber == accountNumber.ToString());
            if (user is null)
                return new ApiResult
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "حسابی با این شماره پیدا نشد "
                };

            user.Balance = amount;
            await context.SaveChangesAsync();
            return new ApiResult
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<ApiResult<string>> AddUser(AddUserDto request)
        {
            var user = new Users()
            {
                ClientId = request.ClientId,
                ClientSecret = request.ClientSecret.HashSha256(),
                Age = request.Age,
                userType = request.UserType,
                AccountNumber=UniqueNumberGenerator.GenerateUnique17DigitNumber().ToString(),
            };
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return new ApiResult<string>
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.Created,
                Data=user.AccountNumber,
            };
        }

        public async Task<ApiResult> ChangePassword(ChangePasswordDto request)
        {
            var user = await context.Users.FindAsync(request.Id);
            if (user is null)
                return new ApiResult
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "موردی یافت نشد"
                };

            if (!SecurityHelper.Verify(request.oldPassword, user.ClientSecret))
                return new ApiResult
                {
                    StatusCode = HttpStatusCode.NotAcceptable,
                    Message = "کلاینت ای دی و یا کلاینت سکرت اشتباه وارد شده"
                };

            user.ClientSecret = request.newPassword.HashSha256();
            await context.SaveChangesAsync();

            return new ApiResult
            {
                IsSuccess = true,
                StatusCode=HttpStatusCode.OK,
            };
        }

        public async Task<ApiResult> DeleteUser(int id)
        {
            var user = await context.Users.FindAsync(id);
            if (user is null)
                return new ApiResult
                {
                    Message = "موردی یافت نشد",
                    StatusCode = HttpStatusCode.NotFound
                };
            context.Users.Remove(user);
            await context.SaveChangesAsync();
            return new ApiResult
            {
                IsSuccess = true,
                StatusCode=HttpStatusCode.OK
            };
        }

        public async Task<ApiResult<Users>> Login(TokenRequest request)
        {
            var user = await context.Users.FirstOrDefaultAsync(x=>x.ClientId==request.Client_Id);
            if (user is null)
                return new ApiResult<Users>
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    Message = "کلاینت ای دی و یا کلاینت سکرت اشتباه وارد شده"
                };

            if (!SecurityHelper.Verify(request.Client_Secret,user.ClientSecret ))
                return new ApiResult<Users>
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    Message = "کلاینت ای دی و یا کلاینت سکرت اشتباه وارد شده"
                };

            return new ApiResult<Users>
            {
                Data = user,
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true
            };
        }

        public async Task<ApiResult> UpdateUser(EdutUserDto edutUserDto)
        {
            var user = await context.Users.FindAsync(edutUserDto.Id);
            if (user is null)
                return new ApiResult
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "موردی یافت نشد"
                };

            user.ClientId = edutUserDto.ClientId;
            user.Age = edutUserDto.Age;
            await context.SaveChangesAsync();
            return new ApiResult
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}