using Microsoft.AspNetCore.Mvc;
using VendingMachine.Services;

namespace VendingMachine.Controllers
{
    [ApiController]
    [Route("api/paymentType")]
    public class PaymentTypeController : ControllerBase
    {
        private readonly IPaymentType _paymentTypeService;

        public PaymentTypeController(
            IPaymentType odemeTipiService)
        {
            _paymentTypeService = odemeTipiService;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var odemeTipleri = _paymentTypeService.GetAll();
            return Ok(odemeTipleri);
        }
        [HttpGet("getById/{id}")]
        public IActionResult GetById(int id)
        {
            var food = _paymentTypeService.GetById(id);
            return Ok(food);
        }
    }
}
