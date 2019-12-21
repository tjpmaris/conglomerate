using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace Conglomerate.ApiLib
{
    [Route("api/[controller]")]
    public class IngredientsController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok("Hello World");
        }
    }
}
