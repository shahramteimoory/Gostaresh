using System.Net;
using MachineReporting.Api.Dtos;
using MachineReporting.Api.Models.DataBaseContext;
using MachineReporting.Api.Models.Entities;
using MachineReporting.Api.Services.TransactionService.Command.Dto;
using Microsoft.EntityFrameworkCore;

namespace MachineReporting.Api.Services.TransactionService.Command
{
    public interface ITransactionManagmentService
    {
        Task<ApiResult> Add();
        Task<ApiResult> Update();
        Task<ApiResult> Delete();
        Task<ApiResult> TransferFundToVipWithId(TransferFundToVipWithIdDto request);
    }

    public class TransactionManagmentService(DataBaseContext context) : ITransactionManagmentService
    {
        public Task<ApiResult> Add()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult> Delete()
        {
            throw new NotImplementedException();
        }

public async Task<ApiResult> TransferFundToVipWithId(TransferFundToVipWithIdDto request)
{
    using var transaction = await context.Database.BeginTransactionAsync();

    try
    {
        var senderAndReceiver = await CheckTransactionSenderAndReciver(request.Sender, request.Reciver);

        if (!senderAndReceiver.IsSuccess)
            return new ApiResult
            {
                StatusCode = senderAndReceiver.StatusCode,
                Message = senderAndReceiver.Message
            };

        if (senderAndReceiver.Data.Reciver.userType != UserType.Vip)
            return new ApiResult
            {
                StatusCode = HttpStatusCode.BadRequest,
                Message = "دریافت کننده Vip نیست"
            };

            if (senderAndReceiver.Data.Sender.Balance < request.Amount)
            {
                return new ApiResult
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "ارسال کننده موجودی کافی ندارد"
                };
            }
        var firstCode = request.Codes.FirstOrDefault(x => x.IsFirstCode)?.Code;
        var uniqCode = await CreateTransactionCode(request.SenderFullName, firstCode);

        var codes = request.Codes
            .Where(x => !x.IsFirstCode)
            .Select(s => new Codes
            {
                Code = s.Code,
                IsFirstCode = false
            })
            .ToList();

        codes.Add(uniqCode);

        var transactionEntity = new Transactions
        {
            Sender = request.Sender,
            Reciver = request.Reciver,
            SenderFullName = request.SenderFullName,
            Decription = request.Decription,
            TraceCode = request.TraceCode,
            ReceiptNo = request.ReceiptNo,
            Amount = request.Amount,
            SenderUserType = request.SenderUserType,
            SpecId = request.SpecId,
            PromoCode = request.PromoCode,
            Codes = codes
        };

        senderAndReceiver.Data.Reciver.Balance += request.Amount;

        context.Transactions.Add(transactionEntity);
        await context.SaveChangesAsync();

        await transaction.CommitAsync();

        return new ApiResult
        {
            StatusCode = HttpStatusCode.OK,
            Message = "انتقال با موفقیت انجام شد"
        };
    }
    catch (Exception ex)
    {
        await transaction.RollbackAsync();
        return new ApiResult
        {
            StatusCode = HttpStatusCode.InternalServerError,
            Message = "خطا در انجام عملیات انتقال"
        };
    }
}


        public Task<ApiResult> Update()
        {
            throw new NotImplementedException();
        }

        private async Task<ApiResult<SenderAndreciverDto>> CheckTransactionSenderAndReciver(string sender, string reciver)
        {
            var senderTask = context.Users.FirstOrDefaultAsync(x => x.AccountNumber == sender);
            var reciverTask = context.Users.FirstOrDefaultAsync(x => x.AccountNumber == reciver);
            await Task.WhenAll(senderTask, reciverTask);
            if (senderTask.Result is null || reciverTask.Result is null)
            {
                return new ApiResult<SenderAndreciverDto>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = senderTask.Result is null && reciverTask.Result is null ? "ارسال‌کننده و گیرنده یافت نشدند" :
                            senderTask.Result is null ? "ارسال‌کننده یافت نشد" :
                            "گیرنده یافت نشد"
                };
            }
            return new ApiResult<SenderAndreciverDto>
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Data = new SenderAndreciverDto
                {
                    Sender = senderTask.Result,
                    Reciver = reciverTask.Result
                }
            };
        }

        private async Task<Codes> CreateTransactionCode(string? fullName, string? firstCode)
        {
            var code = new Codes
            {
            IsFirstCode = true,
            };

            if (string.IsNullOrWhiteSpace(fullName) && string.IsNullOrWhiteSpace(firstCode))
            {
                code.Code = Guid.NewGuid().ToString();
                return code;
            }

            if (string.IsNullOrWhiteSpace(firstCode))
            {
                code.Code = $"{fullName}{Guid.NewGuid()}";
                return code;
            }

            bool isFirstCodeUniq = await context.Codes
            .AnyAsync(c => c.Code == firstCode && c.IsFirstCode == true);

            if (!isFirstCodeUniq)
            {
                code.Code = $"{fullName}{firstCode}";
                return code;
            }

            code.Code = $"{fullName}{Guid.NewGuid()}";
            return code;
        }
    }
}