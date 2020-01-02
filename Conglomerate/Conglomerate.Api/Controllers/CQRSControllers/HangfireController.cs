using System;
using System.Threading.Tasks;

using Conglomerate.Process.Common.Constants;
using Conglomerate.Process.Common.Jobs;
using Conglomerate.Process.OneTime;

using Microsoft.AspNetCore.Mvc;

namespace Conglomerate.Api.Controllers.CQRSControllers
{
    [Route("api/cqrs/[controller]")]
    public class HangfireController : ControllerBase
    {
        public IJobFactory _jobFactory;

        public HangfireController(IJobFactory jobFactory)
        {
            _jobFactory = jobFactory;
        }

        [HttpGet]
        public async Task<IActionResult> DoProcess()
        {
            _jobFactory
                .CreateJob()
                .OnQueue(JobQueues.AGENT)
                .Schedule<MultiParameterProcess>(s => s.Execute("Bob", 22), DateTimeOffset.Now);

            return Ok();
        }
    }
}
