

using ListenPay.Data.Entities;

namespace WebApi.Models
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(Company company, string token)
        {
            Id = company.CompanyId;
            FirstName = company.FirstName;
            LastName = company.LastName;
            Token = token;
        }
    }
}