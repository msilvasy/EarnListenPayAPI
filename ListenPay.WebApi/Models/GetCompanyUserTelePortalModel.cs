using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListenPay.WebApi.Models
{
    public class GetCompanyUserTelePortalModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string StreetAddress { get; set; }
        public string Unit { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string InsuranceCarrier { get; set; }
        public string MemberId { get; set; }
        public string GroupNumber { get; set; }
        public int CompanyId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
