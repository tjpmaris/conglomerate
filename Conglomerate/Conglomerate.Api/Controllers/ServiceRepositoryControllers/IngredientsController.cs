using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using Conglomerate.Api.Models;
using Conglomerate.ServiceRepository.Models;
using Conglomerate.ServiceRepository.Services;

using Microsoft.AspNetCore.Mvc;

namespace Conglomerate.Api.ServiceRepository
{
    [Route("api/servicerepository/[controller]")]
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientService _ingredientService;
        private readonly IMapper _mapper;

        public IngredientsController(IIngredientService ingredientService, IMapper mapper)
        {
            _ingredientService = ingredientService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<List<IngredientDto>>(await _ingredientService.GetAll()));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(_mapper.Map<IngredientDto>(await _ingredientService.Get(id)));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IngredientDto ingredient)
        {
            return Created("", _mapper.Map<IngredientDto>(await _ingredientService.Create(_mapper.Map<IngredientLogic>(ingredient))));
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] IngredientDto ingredient)
        {
            return Ok(_mapper.Map<IngredientDto>(await _ingredientService.Update(id, _mapper.Map<IngredientLogic>(ingredient))));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(_mapper.Map<IngredientDto>(await _ingredientService.Delete(id)));
        }
    }
}
