namespace MachineReporting.Api.Services.UsersServices.Commands.Dto
{
    public record ChangePasswordDto
    {
        public int Id { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }
}