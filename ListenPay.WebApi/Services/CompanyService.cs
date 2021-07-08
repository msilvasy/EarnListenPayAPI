using ListenPay.Data;
using ListenPay.Data.Entities;
using ListenPay.WebApi.BindingModels;
using ListenPay.WebApi.Helpers;
using ListenPay.WebApi.IService;
using ListenPay.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models;

namespace ListenPay.WebApi.Services
{
    public class CompanyService : ICompany
    {

        private readonly JwtSettings _jwtSettings;
        private readonly ListenPayDbContext _context;
        private readonly IKeyGeneratorService _keyGeneratorService;
        public CompanyService(ListenPayDbContext context, IOptions<JwtSettings> jwtSettings, IKeyGeneratorService keyGeneratorService)
        {
            _context = context;
            _jwtSettings = jwtSettings.Value;
            _keyGeneratorService = keyGeneratorService;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequestBindingModel model)
        {
            if (_context != null)
            {
                var user = _context.Company.SingleOrDefault(x => x.Email == model.Email && x.Password == model.Password);

                if (user == null) return null;

                var token = Helper.GenerateJwtToken(user.CompanyId, _jwtSettings.Secret);

                return new AuthenticateResponse(user, token);
            }
            return null;
        }

        public IEnumerable<Company> GetAll()
        {
            return _context.Company;
        }

        public Company GetCompanyById(int id)
        {
            return _context.Company.FirstOrDefault(x => x.CompanyId == id);
        }

        public Company GetCompanyByEmail(string email)
        {
            return _context.Company.FirstOrDefault(x => x.Email.ToLower().Trim() == email.ToLower().Trim());
        }
        public async Task<bool> AddCompany(AddCompanyBindingModel model)
        {
            var isAdded = false;
            if (_context != null)
            {
                var company = new Company()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password,
                    DateEntryCreated = DateTime.Now,
                    DateEntryModified = DateTime.Now
                };
                await _context.Company.AddAsync(company);
                await _context.SaveChangesAsync();
                isAdded = true;
            }
            return isAdded;
        }

        public async Task<TelePortalAccount> AddUpdateCompanyUserTelePortalAccount(AddCompanyUserTelePortalBindingModel model)
        {
            if (_context != null)
            {
                string firstName = "";
                string lastName = "";
                string[] fullName = model.FullName.Split(" ");
                firstName = fullName[0];
                for (int i = 1; i < fullName.Length; i++)
                {
                    lastName += fullName[i];
                }
                var telePortalAccount = _context.TelePortalAccount.Find(model.UserId);
                if (telePortalAccount == null)
                {
                    if (await _context.TelePortalAccount.FirstOrDefaultAsync(e => e.Email == model.Email) != null)
                    {
                        throw new ApplicationException("Email already registered!");
                    }

                    var userInformation = await _context.UserInformation.FirstOrDefaultAsync(u => u.Email.Trim().ToLower() == model.Email.Trim().ToLower());
                    if (userInformation == null)
                    {
                        userInformation = new UserInformation()
                        {
                            Email = model.Email,
                            FirstName = firstName,
                            LastName = lastName ?? "",
                            DateEntryCreated = DateTime.Now,
                            CurrentPassword = model.Password,
                            ConfirmPassword = model.ConfirmPassword,
                            City = model.City,
                            State = model.State,
                            Zip = model.Zip,
                            PhoneNo = model.Phone,
                            Active = true,
                            Earned = 20,
                            Discount = 30,
                            Key = _keyGeneratorService.GetBase36(128)
                        };
                        await _context.UserInformation.AddAsync(userInformation);
                    }
                    telePortalAccount = new TelePortalAccount()
                    {
                        FullName = model.FullName,
                        StreetAddress = model.StreetAddress,
                        Unit = model.Unit,
                        City = model.City,
                        State = model.State,
                        Zip = model.Zip,
                        Phone = model.Phone,
                        CurrentPassword = model.Password,
                        ConfirmPassword = model.ConfirmPassword,
                        InsuranceCarrier = model.InsuranceCarrier,
                        MemberId = model.MemberId,
                        GroupNumber = model.GroupNumber,
                        UserInformation = userInformation,
                        CompanyId = model.CompanyId,
                        Email = model.Email,
                        DateEntryCreated = DateTime.Now,
                        DateEntryModified = DateTime.Now,
                        Active = true
                    };

                    _context.TelePortalAccount.Add(telePortalAccount);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    telePortalAccount.FullName = model.FullName;
                    telePortalAccount.StreetAddress = model.StreetAddress;
                    telePortalAccount.Unit = model.Unit;
                    telePortalAccount.City = model.City;
                    telePortalAccount.State = model.State;
                    telePortalAccount.Zip = model.Zip;
                    telePortalAccount.Phone = model.Phone;
                    telePortalAccount.Email = model.Email;
                    telePortalAccount.CurrentPassword = model.Password;
                    telePortalAccount.ConfirmPassword = model.ConfirmPassword;
                    telePortalAccount.InsuranceCarrier = model.InsuranceCarrier;
                    telePortalAccount.MemberId = model.MemberId;
                    telePortalAccount.GroupNumber = model.GroupNumber;
                    telePortalAccount.DateEntryModified = DateTime.Now;
                    telePortalAccount.Active = true;
                    _context.TelePortalAccount.Update(telePortalAccount);
                    await _context.SaveChangesAsync();
                }
                return telePortalAccount;
            }
            return null;
        }
        public async Task<List<GetCompanyUserTelePortalModel>> GetCompanyUserTelePortalAccount(int companyId)
        {
            if (_context != null)
            {
                return await (
                    from teleportalAccount in _context.TelePortalAccount
                    where teleportalAccount.CompanyId == companyId && teleportalAccount.Active
                    select new GetCompanyUserTelePortalModel()
                    {
                        UserId = teleportalAccount.UserId,
                        FullName = teleportalAccount.FullName,
                        StreetAddress = teleportalAccount.StreetAddress,
                        Unit = teleportalAccount.Unit,
                        City = teleportalAccount.City,
                        State = teleportalAccount.State,
                        Zip = teleportalAccount.Zip,
                        Phone = teleportalAccount.Phone,
                        InsuranceCarrier = teleportalAccount.InsuranceCarrier,
                        MemberId = teleportalAccount.MemberId,
                        GroupNumber = teleportalAccount.GroupNumber,
                        CompanyId = teleportalAccount.CompanyId,
                        Email = teleportalAccount.Email,
                        DateCreated = (DateTime)teleportalAccount.DateEntryModified
                    }).ToListAsync();
            }
            return null;
        }
        public async Task<bool> DeleteCompanyUserTelePortalAccount(int userId)
        {
            int result = 0;
            if (_context != null)
            {
                var telePortalAccount = await _context.TelePortalAccount.Include(y => y.UserInformation).SingleOrDefaultAsync(x => x.UserId == userId);
                if (telePortalAccount != null)
                {
                    telePortalAccount.Active = false;
                    telePortalAccount.UserInformation.Active = false;
                    _context.UserInformation.Update(telePortalAccount.UserInformation);
                    _context.TelePortalAccount.Remove(telePortalAccount);
                    result = await _context.SaveChangesAsync();
                }
            }
            return result > 0;
        }

        #region IDisposable Support
        private bool _disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                }
                _context.DisposeAsync();
                _disposedValue = true;
            }
        }

        ~CompanyService()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
