using RestApiSample.Models.Dto;
using RestApiSample.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VendingMachine.Helpers;

namespace VendingMachine.Services
{
    public class FoodDao : IFood
    {
        private readonly DataContext _context;
        private readonly IPaymentType _paymentType;
        public FoodDao(DataContext context, IPaymentType paymentType)
        {
            _context = context;
            _paymentType = paymentType;
        }

        public ResultDto Create(FoodCreateDto model)
        {
            if (_context.Foods.Any(x => x.productName == model.productName && x.deleted == 0))
                return new ResultDto() { ResultCode = "001", ResultDescription = "Food with the product Name '" + model.productName + "' already exists" };

            var food = new Food
            {
                id = _context.Foods.Count() + 1,
                creationDate = DateTime.Now,
                deleted = 0,
                updatedBy = "",
                updateDate = null,
                productDesc = model.productDesc,
                productName = model.productName,
                productPrice = model.productPrice,
                productMount = model.productMount,
                createdBy = model.createdBy,
            };

            _context.Foods.Add(food);
            _context.SaveChanges();
            return new ResultDto() { ResultCode = "000", ResultDescription = "Food created" };
        }

        public ResultDto Delete(int id)
        {
            Food food = getFood(id);
            if (food == null) return new ResultDto() { ResultCode = "001", ResultDescription = "Food not found" };

            food.deleted = 1;
            _context.Foods.Update(food);
            _context.SaveChanges();
            return new ResultDto() { ResultCode = "000", ResultDescription = "Food deleted" };
        }

        public List<Food> GetAll()
        {
            return _context.Foods.Where(i => i.deleted != 1).ToList();
        }

        public Food GetById(int id)
        {
            return getFood(id);
        }

        public ResultDto Update(int id, FoodUpdateDto model)
        {
            Food food = getFood(id);
            if (food == null) return new ResultDto() { ResultCode = "001", ResultDescription = "Food not found" };

            if (model.productName == null)
                return new ResultDto() { ResultCode = "001", ResultDescription = "Object is not null" };

            if (model.productName != food.productName && _context.Drinks.Any(x => x.productName == model.productName && x.deleted == 0))
                return new ResultDto() { ResultCode = "001", ResultDescription = "Drink with the product Name '" + model.productName + "' already exists" };

            food.updateDate = DateTime.Now;
            food.updatedBy = model.updatedBy;
            food.productDesc = model.productDesc;
            food.productName = model.productName;
            food.productPrice = model.productPrice;
            food.productMount = model.productMount;

            _context.Foods.Update(food);
            _context.SaveChanges();
            return new ResultDto() { ResultCode = "000", ResultDescription = "Food updated" };
        }

        private Food getFood(int id)
        {
            var food = _context.Foods.Find(id);
            if (food == null || food.deleted == 1) return null;
            return food;
        }

        public ResultDto TakeFood(ProductFoodDto model)
        {
            Food food = getFood(model.productId);
            if (food == null) return new ResultDto() { ResultCode = "001", ResultDescription = "Food not found" };

            if (_context.Foods.Any(x => x.id == model.productId && x.productMount == 0))
                return new ResultDto() { ResultCode = "001", ResultDescription = "Food does not exists" };

            if (model.productMount * food.productPrice > model.productPrice)
                return new ResultDto() { ResultCode = "001", ResultDescription = "Product Pice is '" + food.productPrice * model.productMount + "' Turkish Liras." };

            if (model.productMount > food.productMount)
                return new ResultDto() { ResultCode = "001", ResultDescription = "You can buy up to '" + food.productMount + "' items" };

            PaymentType paymentType = _paymentType.GetById(model.paymentType);
            if (paymentType == null)
                return new ResultDto() { ResultCode = "001", ResultDescription = "Payment type not found" };

            food.updateDate = DateTime.Now;
            food.updatedBy = model.createdBy;
            food.productMount = food.productMount - model.productMount;

            _context.Foods.Update(food);
            _context.SaveChanges();

            int refundedAmount = model.productPrice - (model.productMount * food.productPrice.Value);

            StringBuilder resultInfo = new StringBuilder();
            resultInfo.AppendLine("Product Name: " + food.productDesc);
            resultInfo.AppendLine("Product Mount: " + model.productMount);
            resultInfo.AppendLine("Payment Type: " + paymentType.description);
            resultInfo.AppendLine("Refunded amount: " + refundedAmount);

            return new ResultDto() { ResultCode = "000", ResultDescription = resultInfo.ToString() };
        }
    }
}
