using Microsoft.AspNetCore.Mvc;
using RestApiSample.Models.Dto;
using VendingMachine.Services;

namespace VendingMachine.Controllers
{
    [Route("api/foods")]
    [ApiController]
    public class FoodController : Controller
    {
        private readonly IFood _foodService;

        public FoodController(
            IFood foodService)
        {
            _foodService = foodService;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var drinks = _foodService.GetAll();
            return Ok(drinks);
        }

        [HttpGet("getById/{id}")]
        public IActionResult GetById(int id)
        {
            var food = _foodService.GetById(id);
            return Ok(food);
        }

        [HttpPost("newFood")]
        public IActionResult Create(FoodCreateDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var returnObj = _foodService.Create(model);
            return Ok(returnObj);
        }

        [HttpPut("updateFood/{id}")]
        public IActionResult Update(int id, FoodUpdateDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var returnObj = _foodService.Update(id, model);
            return Ok(returnObj);
        }

        [HttpDelete("deleteFood/{id}")]
        public IActionResult Delete(int id)
        {
            var returnObj = _foodService.Delete(id);
            return Ok(returnObj);
        }
        [HttpPost("takeFood")]
        public IActionResult TakeFood(ProductFoodDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var returnObj = _foodService.TakeFood(model);
            return Ok(returnObj);
        }
    }
}
