using MachineReporting.Api.Models.Entities;

namespace MachineReporting.Api.Services.TransactionService.Command.Dto
{
    public record SenderAndreciverDto
    {
        public Users Sender { get; set; }
        public Users Reciver { get; set; }
    }
}