using MachineReporting.Api.Models.Entities;

namespace MachineReporting.Api.Services.TransactionService.Command.Dto
{
    public class TransferFundToVipWithIdDto
    {
        public required string Sender { get; set; }
        public required string Reciver { get; set; }
        public string? SenderFullName { get; set; }
        public string? Decription { get; set; }
        public string? TraceCode { get; set; }
        public string? ReceiptNo { get; set; }
        public required decimal Amount { get; set; }
        public UserType SenderUserType { get; set; }
        public string? SpecId { get; set; }
        public string? PromoCode { get; set; }
        public ICollection<TransferCode> Codes { get; set; }=new List<TransferCode>();
    }
}