using Microsoft.AspNetCore.Mvc;
using Online_Store.Models;
using Online_Store.UnitOfWork;

namespace Online_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderItemController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orderItems = await _unitOfWork.Items.GetAllAsync();
            return Ok(orderItems);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var orderItem = await _unitOfWork.Items.GetByIdAsync(id);
            if (orderItem == null)
                return NotFound();

            return Ok(orderItem);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderItem orderItem)
        {
            await _unitOfWork.Items.AddAsync(orderItem);
            await _unitOfWork.CompleteAsync();
            return CreatedAtAction(nameof(GetById), new { id = orderItem.OrderItemId }, orderItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OrderItem orderItem)
        {
            if (id != orderItem.OrderItemId)
                return BadRequest();

            _unitOfWork.Items.Update(orderItem);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var orderItem = await _unitOfWork.Items.GetByIdAsync(id);
            if (orderItem == null)
                return NotFound();

            _unitOfWork.Items.Remove(orderItem);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
    }
}
