using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Online_Store.Models;
using Online_Store.UnitOfWork;
using Online_Store.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _unitOfWork.Orders.GetAllAsync();
            var orderDTOs = _mapper.Map<IEnumerable<OrderDTO>>(orders);
            return Ok(orderDTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(id);
            if (order == null)
                return NotFound();

            var orderDTO = _mapper.Map<OrderDTO>(order);
            return Ok(orderDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderDTO orderDTO)
        {
            var order = _mapper.Map<Order>(orderDTO);
            await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.CompleteAsync();
            return CreatedAtAction(nameof(GetById), new { id = order.OrderId }, orderDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OrderDTO orderDTO)
        {
            if (id != orderDTO.OrderId)
                return BadRequest();

            var order = await _unitOfWork.Orders.GetByIdAsync(id);
            if (order == null)
                return NotFound();

            _mapper.Map(orderDTO, order);
            _unitOfWork.Orders.Update(order);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(id);
            if (order == null)
                return NotFound();

            _unitOfWork.Orders.Remove(order);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
    }
}
