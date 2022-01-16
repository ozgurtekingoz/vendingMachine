using RestApiSample.Models.Dto;
using RestApiSample.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VendingMachine.Helpers;

namespace VendingMachine.Services
{
    public class DrinkDao : IDrink
    {
        private readonly DataContext _context;
        private readonly IPaymentType _paymentType;
        public DrinkDao(DataContext context, IPaymentType paymentType)
        {
            _context = context;
            _paymentType = paymentType;
        }

        public ResultDto Create(DrinkCreateDto model)
        {
            if (_context.Drinks.Any(x => x.productName == model.ProductName && x.deleted == 0))
                return new ResultDto() { ResultCode = "001", ResultDescription = "Drink with the product Name '" + model.ProductName + "' already exists" };

            var drink = new Drink
            {
                id = _context.Foods.Count() + 1,
                creationDate = DateTime.Now,
                deleted = 0,
                updatedBy = "",
                updateDate = null,
                productDesc = model.ProductDesc,
                productName = model.ProductName,
                productPrice = model.ProductPrice,
                productMount = model.ProductMount,
                createdBy = model.CreatedBy,
            };

            _context.Drinks.Add(drink);
            _context.SaveChanges();
            return new ResultDto() { ResultCode = "000", ResultDescription = "Food created" };
        }
        public ResultDto Delete(int id)
        {
            Drink drink = getDrink(id);
            if (drink == null) return new ResultDto() { ResultCode = "001", ResultDescription = "Drink not found" };

            drink.deleted = 1;
            _context.Drinks.Update(drink);
            _context.SaveChanges();
            return new ResultDto() { ResultCode = "000", ResultDescription = "Food deleted" };
        }

        public List<Drink> GetAll()
        {
            return _context.Drinks.Where(i => i.deleted != 1).ToList();
        }

        public Drink GetById(int id)
        {
            return getDrink(id);
        }

        public ResultDto Update(int id, DrinkUpdateDto model)
        {
            Drink drink = getDrink(id);
            if (drink == null) return new ResultDto() { ResultCode = "001", ResultDescription = "Drink not found" };

            if (model.productName == null)
                return new ResultDto() { ResultCode = "001", ResultDescription = "Object is not null" };

            if (model.productName != drink.productName && _context.Drinks.Any(x => x.productName == model.productName && x.deleted == 0))
                return new ResultDto() { ResultCode = "001", ResultDescription = "Drink with the product Name '" + model.productName + "' already exists" };

            drink.updateDate = DateTime.Now;
            drink.updatedBy = model.updatedBy;
            drink.productDesc = model.productDesc;
            drink.productName = model.productName;
            drink.productPrice = model.productPrice;
            drink.productMount = model.productMount;

            _context.Drinks.Update(drink);
            _context.SaveChanges();
            return new ResultDto() { ResultCode = "000", ResultDescription = "Drink updated" };

        }

        private Drink getDrink(int id)
        {
            var drink = _context.Drinks.Find(id);
            if (drink == null || drink.deleted == 1) return null;
            return drink;
        }

        public ResultDto TakeDrink(ProductDrinkDto model)
        {
            Drink drink = getDrink(model.productId);
            if (drink == null) return new ResultDto() { ResultCode = "001", ResultDescription = "Drink not found" };

            if (_context.Drinks.Any(x => x.id == model.productId && x.productMount == 0))
                return new ResultDto() { ResultCode = "001", ResultDescription = "Drink does not exists" };

            if (model.productMount * drink.productPrice > model.productPrice)
                return new ResultDto() { ResultCode = "001", ResultDescription = "Product Pice is '" + drink.productPrice * model.productMount + "' Turkish Liras." };

            if (model.productMount > drink.productMount)
                return new ResultDto() { ResultCode = "001", ResultDescription = "You can buy up to '" + drink.productMount + "' items" };


            drink.updateDate = DateTime.Now;
            drink.updatedBy = model.CreatedBy;
            drink.productMount = drink.productMount - model.productMount;

            _context.Drinks.Update(drink);
            _context.SaveChanges();

            PaymentType paymentType = _paymentType.GetById(model.paymentType);
            if (paymentType == null)
                return new ResultDto() { ResultCode = "001", ResultDescription = "Payment type not found" };

            int refundedAmount = model.productPrice - (model.productMount * drink.productPrice.Value);

            StringBuilder resultInfo = new StringBuilder();
            resultInfo.AppendLine("Product Name: " + drink.productDesc);
            resultInfo.AppendLine("Product Mount: " + model.productMount);
            resultInfo.AppendLine("Payment Type: " + paymentType.description);
            resultInfo.AppendLine("Refunded amount: " + refundedAmount);

            return new ResultDto() { ResultCode = "000", ResultDescription = resultInfo.ToString() };
        }

    }
}
