using RestApiSample.Models.Entities;
using System.Collections.Generic;

namespace VendingMachine.Services
{
    public interface IPaymentType
    {
        List<PaymentType> GetAll();

        PaymentType GetById(int id);
    }
}
