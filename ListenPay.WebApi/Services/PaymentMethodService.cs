using ListenPay.Data;
using ListenPay.Data.Entities;
using ListenPay.WebApi.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListenPay.WebApi.Services
{
    public class PaymentMethodService : IPaymentMethodService
    {

        private readonly ListenPayDbContext _context;

        public PaymentMethodService(ListenPayDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddPaymentMethod(string paymentMethodTypeTitle, int userInformationId)
        {
            var isAdded = false;
            if (_context != null)
            {
                var paymentMethod = new PaymentMethod()
                {
                    PaymentMethodTypeTitle = paymentMethodTypeTitle,
                    UserInformationId = userInformationId,
                    DateEntryCreated = DateTime.Now,
                    DateEntryModified = DateTime.Now
                };
                await _context.PaymentMethod.AddAsync(paymentMethod);
                await _context.SaveChangesAsync();
                isAdded = true;
            }
            return isAdded;
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

        ~PaymentMethodService()
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
