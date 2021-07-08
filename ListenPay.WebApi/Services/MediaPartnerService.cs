using ListenPay.Data;
using ListenPay.Data.Entities;
using ListenPay.WebApi.IService;
using ListenPay.WebApi.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ListenPay.WebApi.Services
{
    public class MediaPartnerService : IMediaPartner
    {

        private readonly ListenPayDbContext _context;
        private readonly IOptionsMonitor<SMTPOption> _smtpOptions;
        public MediaPartnerService(ListenPayDbContext context, IOptionsMonitor<SMTPOption> smtpOptions)
        {
            _context = context;
            _smtpOptions = smtpOptions;
        }
        public async Task<bool> AddMediaPartner(string companyArtist, string WebSiteURL, string email, string phone, string selectedProduct, string comments)
        {

            int result = 0;
            if (_context != null)
            {
                var mediaPartner = new MediaPartner()
                {
                    CompnayArtist = companyArtist,
                    URL = WebSiteURL,
                    Email = email,
                    Phone = phone,
                    Comments = comments,
                    DateEntryCreated = DateTime.Now,
                    DateEntryModified = DateTime.Now
                };
                var products = selectedProduct.Split('|');
                mediaPartner.MediaPartnerProduct = new List<MediaPartnerProduct>();
                if (products.Length > 0)
                {
                    foreach (var product in products)
                    {
                        if (product.Length > 1)
                            mediaPartner.MediaPartnerProduct.Add(new MediaPartnerProduct()
                            {
                                ProductTitle = product,
                                DateEntryCreated = DateTime.Now,
                                DateEntryModified = DateTime.Now
                            });
                    }
                }
                _context.MediaPartner.Add(mediaPartner);
                result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    SendEmail(mediaPartner);

                }
                else
                {
                    return false;
                }
            }
            return true;

        }
        public bool SendEmail(MediaPartner mediaPartner)
        {

            try
            {
                string body = $"This is confirmation email from ListenPay.IO <br><br>" +
                    $"<div>Company | Artist: {mediaPartner.CompnayArtist} </div> <div>Website URL: {mediaPartner.URL} </div> <div>Phone: {mediaPartner.Phone} </div> <br><br> Product(s) <ul>";
                foreach (var product in mediaPartner.MediaPartnerProduct)
                {
                    body += $"<li>{product.ProductTitle}</li>";
                }
                body += "</ul> <br><br> <div><i>This is system generated email. please donot reply.</i> </div>";

                MailMessage message = new MailMessage(new MailAddress(_smtpOptions.CurrentValue.FromEmailAddress), new MailAddress(mediaPartner.Email));
                message.Subject = "Thank you for selecting ListenPay.IO";
                message.IsBodyHtml = true;
                message.Body = body;

                var client = new SmtpClient();
                client.Credentials = new NetworkCredential(_smtpOptions.CurrentValue.FromEmailAddress, _smtpOptions.CurrentValue.FromEmailPassword);
                client.Host = _smtpOptions.CurrentValue.SMTPHost;
                client.EnableSsl = true;
                client.Port = _smtpOptions.CurrentValue.SMTPPort;
                client.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                throw (new Exception("Mail send failed "));
            }

        }
    }
}
