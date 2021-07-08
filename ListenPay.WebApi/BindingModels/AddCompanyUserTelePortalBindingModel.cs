using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ListenPay.WebApi.BindingModels
{
    public class AddCompanyUserTelePortalBindingModel
    {
        public int UserId { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string StreetAddress { get; set; }
        public string Unit { get; set; }
        [Required]
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string InsuranceCarrier { get; set; }
        public string MemberId { get; set; }
        public string GroupNumber { get; set; }
        [Required]
        public int CompanyId { get; set; }
    }
}
