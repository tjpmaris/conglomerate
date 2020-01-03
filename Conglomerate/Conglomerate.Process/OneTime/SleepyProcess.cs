using System.Threading;
using System.Threading.Tasks;

namespace Conglomerate.Process.OneTime
{
    public class SleepyProcess
    {
        public async Task Execute()
        {
            Thread.Sleep(10000);
        }
    }
}
