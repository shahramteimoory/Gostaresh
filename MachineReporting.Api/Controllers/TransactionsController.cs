using MachineReporting.Api.Services.TransactionService.Command;
using MachineReporting.Api.Services.TransactionService.Command.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MachineReporting.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionsController(ITransactionManagmentService transactionManagmentService) : ControllerBase
    {
        [HttpPost("TransferFundToVipWithId")]
        public async Task<ActionResult> TransferFundToVipWithId(TransferFundToVipWithIdDto request)
        {
            return Ok(await transactionManagmentService.TransferFundToVipWithId(request));
        }
    }
}


