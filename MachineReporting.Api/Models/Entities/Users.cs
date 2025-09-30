using System.Security.AccessControl;

namespace MachineReporting.Api.Models.Entities
{
    public class Users : BaseEntity
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public required string ClientId { get; set; }
        public required string ClientSecret { get; set; }
        public int Age { get; set; }
        public ICollection<Transactions> Transactions { get; set; }
        public UserType userType { get; set; }
        public decimal Balance { get; set; } = 0;
    }

    public enum UserType
    {
        Normal,
        Vip
    }
}