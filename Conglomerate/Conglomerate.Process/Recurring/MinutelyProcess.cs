using System;
using System.Threading.Tasks;

namespace Conglomerate.Process.Recurring
{
    public class MinutelyProcess
    {
        public async Task Execute()
        {
            Console.WriteLine("This process happens every minute");
        }
    }
}
