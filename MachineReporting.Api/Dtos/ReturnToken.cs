namespace MachineReporting.Api.Dtos
{
    public record ReturnToken
    {
        public string token { get; set; }
        public DateTime ExpireTime { get; set; }
    }

    public record TokenRequest
    {
        public string Client_Id { get; set; }
        public string Client_Secret { get; set; }
    }
}