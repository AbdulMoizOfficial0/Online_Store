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
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();
            var categoryDTOs = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
            return Ok(categoryDTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            var categoryDTO = _mapper.Map<CategoryDTO>(category);
            return Ok(categoryDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTO categoryDTO)
        {
            var category = _mapper.Map<Category>(categoryDTO);
            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.CompleteAsync();
            return CreatedAtAction(nameof(GetById), new { id = category.CategoryId }, categoryDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoryDTO categoryDTO)
        {
            if (id != categoryDTO.CategoryId)
                return BadRequest();

            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            _mapper.Map(categoryDTO, category);
            _unitOfWork.Categories.Update(category);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            _unitOfWork.Categories.Remove(category);
            await _unitOfWork.CompleteAsync();
            return NoContent();
        }
    }
}
