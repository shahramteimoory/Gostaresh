using MachineReporting.Api.Models.Entities;

namespace MachineReporting.Api.Services.UsersServices.Commands.Dto
{
    public record AddUserDto
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public int Age { get; set; }
        public UserType  UserType { get; set; }
    }
}