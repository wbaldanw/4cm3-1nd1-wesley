namespace Acme.TechnicalTest.Domain.DTO.UserManagement
{
    public class TokenResultDTO
    {
        public required string Token { get; set; }
        public DateTimeOffset Expiration { get; set; }        
    }
}
