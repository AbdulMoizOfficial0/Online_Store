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
    public class OrderItemController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderItemController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orderItems = await _unitOfWork.Items.GetAllAsync();
            var orderItemDTOs = _mapper.Map<IEnumerable<OrderItemDTO>>(orderItems);
            return Ok(orderItemDTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var orderItem = await _unitOfWork.Items.GetByIdAsync(id);
            if (orderItem == null)
                return NotFound();

            var orderItemDTO = _mapper.Map<OrderItemDTO>(orderItem);
            return Ok(orderItemDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderItemDTO orderItemDTO)
        {
            var orderItem = _mapper.Map<OrderItem>(orderItemDTO);
            await _unitOfWork.Items.AddAsync(orderItem);
            await _unitOfWork.CompleteAsync();
            return CreatedAtAction(nameof(GetById), new { id = orderItem.OrderItemId }, orderItemDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OrderItemDTO orderItemDTO)
        {
            if (id != orderItemDTO.OrderItemId)
                return BadRequest();

            var orderItem = await _unitOfWork.Items.GetByIdAsync(id);
            if (orderItem == null)
                return NotFound();

            _mapper.Map(orderItemDTO, orderItem);
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
