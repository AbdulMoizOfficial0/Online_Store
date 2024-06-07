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
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _unitOfWork.Customers.GetAllAsync();
            var customerDTOs = _mapper.Map<IEnumerable<CustomerDTO>>(customers);
            return Ok(customerDTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(id);
            if (customer == null)
                return NotFound();

            var customerDTO = _mapper.Map<CustomerDTO>(customer);
            return Ok(customerDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerDTO customerDTO)
        {
            var customer = _mapper.Map<Customer>(customerDTO);
            await _unitOfWork.Customers.AddAsync(customer);
            await _unitOfWork.CompleteAsync();
            return CreatedAtAction(nameof(GetById), new { id = customer.CustomerId }, customerDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CustomerDTO customerDTO)
        {
            if (id != customerDTO.CustomerId)
                return BadRequest();

            var customer = await _unitOfWork.Customers.GetByIdAsync(id);
            if (customer == null)
                return NotFound();

            _mapper.Map(customerDTO, customer);
            _unitOfWork.Customers.Update(customer);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(id);
            if (customer == null)
                return NotFound();

            _unitOfWork.Customers.Remove(customer);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
    }
}
