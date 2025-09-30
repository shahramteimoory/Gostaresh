namespace MachineReporting.Api.Models.Entities
{
    public class Transactions : BaseEntity
    {
        public int Id { get; set; }
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
        public ICollection<Codes> Codes { get; set; }
    }

    public class Codes : BaseEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public bool IsFirstCode { get; set; }
        public Transactions Transactions { get; set; }
        public int TransactionsId { get; set; }
    }


}