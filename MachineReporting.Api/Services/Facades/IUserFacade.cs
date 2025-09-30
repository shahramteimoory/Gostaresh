using MachineReporting.Api.Services.Query;
using MachineReporting.Api.Services.UsersServices.Commands;

namespace MachineReporting.Api.Services.Facades
{
    public interface IUserFacade
    {
      IUserManagmentServices UserManagmentServices { get; }
      IGetUserManagmentServices GetUserManagmentServices {get; }
    }
}