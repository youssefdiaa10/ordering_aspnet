using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.APIs.Errors;
using Ordering.Core.Models;
using Ordering.Core.Repositories.Interfaces;

namespace Ordering.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : BaseController
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet("{id}")] // GET : /api/basket/id=11
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);

            return Ok(basket ?? new CustomerBasket(id));
        }


        [HttpPost] // POST : /api/basket
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
        {
            var createdOrUpdatedBasket =  _basketRepository.UpdateBasketAsync(basket);

            if (createdOrUpdatedBasket is null) return BadRequest(new ApiResponse(400));
            
            return Ok(createdOrUpdatedBasket);
        }


        [HttpDelete]  // DELETE : /api/basket
        public async Task DeleteBasket(string id)
        {
            await _basketRepository.DeleteBasketAsync(id);
        }
    }
}
