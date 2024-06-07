using AutoMapper;
using Microsoft.AspNetCore.Http;
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
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            var productDto = _mapper.Map<IEnumerable<ProductDTO>>(products);
            return Ok(productDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            var productDto = _mapper.Map<ProductDTO>(product);
            return Ok(productDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.CompleteAsync();
            return CreatedAtAction(nameof(GetById), new { id = product.ProductId }, productDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductDTO productDto)
        {
            if (id != productDto.ProductId)
                return BadRequest();

            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            _mapper.Map(productDto, product);
            _unitOfWork.Products.Update(product);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            _unitOfWork.Products.Remove(product);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
    }
}
