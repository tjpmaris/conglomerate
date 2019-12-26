using System.Threading.Tasks;

using Conglomerate.CQRS.Queries;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Conglomerate.Api.CQRS
{
    [Route("api/cqrs/[controller]")]
    public class IngredientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IngredientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _mediator.Send(new IngredientGetAll.Query()));

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(IngredientGetById.Query query) =>
            Ok(await _mediator.Send(query));


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IngredientCreate.Command command) =>
            Created("", await _mediator.Send(command));

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] IngredientUpdate.UpdateFields updateFields) =>
            Ok(await _mediator.Send(new IngredientUpdate.Command
            {
                Id = id, UpdateFields = updateFields
            }));

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(IngredientDelete.Command command) =>
            Ok(await _mediator.Send(command));
    }
}
