using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RestApiSample.Models.Dto;
using VendingMachine.Services;

namespace VendingMachine.Controllers
{
    [ApiController]
    [Route("api/drinks")]
    
    public class DrinkController : ControllerBase
    {
        private IDrink _drinkService;

        public DrinkController(
            IDrink drinkService)
        {
            _drinkService = drinkService;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var drinks = _drinkService.GetAll();
            return Ok(drinks);
        }

        [HttpGet("getById/{id}")]
        public IActionResult GetById(int id)
        {
            var drink = _drinkService.GetById(id);
            return Ok(drink);
        }

        [HttpPost("newDrink")]
        public IActionResult Create(DrinkCreateDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var returnObj = _drinkService.Create(model);
            return Ok(returnObj);// new { message = "Drink created" });
        }

        [HttpPut("updateDrink/{id}")]
        public IActionResult Update(int id, DrinkUpdateDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var returnObj = _drinkService.Update(id, model);
            return Ok(returnObj);// new { message = "Drink updated" });
        }

        [HttpDelete("deleteDrink/{id}")]
        public IActionResult Delete(int id)
        {
            var returnObj = _drinkService.Delete(id);
            return Ok(returnObj);// new { message = "Drink deleted" });
        }

        [HttpPost("takeDrink")]
        public IActionResult TakeDrink(ProductDrinkDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var returnObj = _drinkService.TakeDrink(model);
            return Ok(returnObj);// new { message = "Drink created" });
        }
    }
}
