using RestApiSample.Models.Dto;
using RestApiSample.Models.Entities;
using System.Collections.Generic;

namespace VendingMachine.Services
{
    public interface IDrink
    {
        List<Drink> GetAll();
        Drink GetById(int id);
        ResultDto Create(DrinkCreateDto model);
        ResultDto Update(int id, DrinkUpdateDto model);
        ResultDto Delete(int id);
        ResultDto TakeDrink(ProductDrinkDto model);
    }
}
