namespace MachineReporting.Api.Services.UsersServices.Commands.Dto
{
    public record EdutUserDto
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public int Age { get; set; }
    }
}