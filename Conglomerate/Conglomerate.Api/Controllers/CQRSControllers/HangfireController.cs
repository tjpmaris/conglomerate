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
            var jobId =
                _jobFactory
                    .CreateJob()
                    .OnQueue(JobQueues.AGENT)
                    .Enqueue<SleepyProcess>(s => s.Execute());

            _jobFactory
                .CreateJob()
                .OnQueue(JobQueues.AGENT)
                .WaitFor(jobId)
                .Enqueue<MultiParameterProcess>(s => s.Execute("Bob", 22));

            return Ok();
        }
    }
}
