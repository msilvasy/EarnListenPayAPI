using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ListenPay.Data.Entities
{
    public class UserInformation : BaseEntity
    {
        [Key]
        public int UserInformationId { get; set; }
        public string Email { get; set; }
        public string Key { get; set; }
        public decimal? Earned { get; set; }
        public decimal Discount { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public int? CountryId { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNo { get; set; }
        public bool Active { get; set; }
        [NotMapped]
        public string Token { get; set; }
        [NotMapped]
        public int CompanyId { get; set; }
    }
}