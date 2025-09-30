using MachineReporting.Api.Models.Entities;

namespace MachineReporting.Api.Services.TransactionService.Command.Dto
{
    public class TransferCode
    {
        public string Code { get; set; }
        public bool IsFirstCode { get; set; }
    }
}