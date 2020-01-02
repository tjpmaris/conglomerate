using System;
using System.Threading.Tasks;

namespace Conglomerate.Process.OneTime
{
    public class MultiParameterProcess
    {
        public async Task Execute(string name, int age)
        {
            Console.WriteLine($"{name} is {age} years old!");
        }
    }
}
