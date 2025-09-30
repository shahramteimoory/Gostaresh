using MachineReporting.Api.Models.Entities;

namespace MachineReporting.Api.Services.Query.Dto
{
    public record UserDto
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public required string ClientId { get; set; }
        public int Age { get; set; }
        public UserType userType { get; set; }
        public decimal Balance { get; set; } = 0;
    }
}