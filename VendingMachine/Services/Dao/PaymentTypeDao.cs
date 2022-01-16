using RestApiSample.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using VendingMachine.Helpers;

namespace VendingMachine.Services
{
    public class PaymentTypeDao : IPaymentType
    {
        private readonly DataContext _context;

        public PaymentTypeDao(DataContext context)
        {
            _context = context;
        }

        public List<PaymentType> GetAll()
        {
            return _context.PaymentType.Where(i => i.deleted != 1).ToList();
        }

        public PaymentType GetById(int id)
        {
            return getPaymentType(id);
        }
        private PaymentType getPaymentType(int id)
        {
            var food = _context.PaymentType.Find(id);
            if (food == null || food.deleted == 1) return null;
            return food;
        }
    }
}
