using RestApiSample.Models.Dto;
using RestApiSample.Models.Entities;
using System.Collections.Generic;

namespace VendingMachine.Services
{
    public interface IFood
    {
        List<Food> GetAll();
        Food GetById(int id);
        ResultDto Create(FoodCreateDto model);
        ResultDto Update(int id, FoodUpdateDto model);
        ResultDto Delete(int id);
        ResultDto TakeFood(ProductFoodDto model);
    }
}
