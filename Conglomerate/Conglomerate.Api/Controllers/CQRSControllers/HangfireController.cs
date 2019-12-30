using System.Threading.Tasks;

using Conglomerate.Process.OneTime;

using Hangfire;
using Hangfire.States;

using Microsoft.AspNetCore.Mvc;

namespace Conglomerate.Api.Controllers.CQRSControllers
{
    [Route("api/cqrs/[controller]")]
    public class HangfireController : ControllerBase
    {
        public IBackgroundJobClient _jobClient;

        public HangfireController(IBackgroundJobClient jobClient)
        {
            _jobClient = jobClient;
        }

        [HttpGet]
        public async Task<IActionResult> DoProcess()
        {
            var state = new EnqueuedState("agent");

            _jobClient.Create<OneTimeProcess>(s => s.Execute(), state);

            return Ok();
        }
    }
}
