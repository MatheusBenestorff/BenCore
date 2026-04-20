using System.Threading.Tasks;
using BenCore.Core;

namespace BenCore
{
    class Program
    {
        static async Task Main(string[] args)
        {
            BenCoreHost host = new BenCoreHost(5000);
            await host.StartAsync();
        }
    }
}