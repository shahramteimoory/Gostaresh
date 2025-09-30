using MachineReporting.Api.Models.DataBaseContext;
using MachineReporting.Api.Services.Facades;
using MachineReporting.Api.Services.Query;
using MachineReporting.Api.Services.UsersServices.Commands;

namespace MachineReporting.Api.Services.UsersServices.Facade
{
    public class UserFacade(DataBaseContext context) : IUserFacade
    {
        public IUserManagmentServices UserManagmentServices =>
        new UserManagmentServices(context);

        public IGetUserManagmentServices GetUserManagmentServices =>
        new GetUserManagmentServices(context);
    }
}