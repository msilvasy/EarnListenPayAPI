using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ListenPay.Data.Entities
{
    public class TelePortalAccount : BaseEntity
    {
        [Key]
        public int UserId { get; set; }
        public UserInformation UserInformation { get; set; }
        public string FullName { get; set; }
        public string StreetAddress { get; set; }
        public string Unit { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string InsuranceCarrier { get; set; }
        public string MemberId { get; set; }
        public string GroupNumber { get; set; }
        public bool Active { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }


    }
}