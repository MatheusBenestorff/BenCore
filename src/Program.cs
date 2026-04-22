using BenCore.Core;
using BenCore.IoC;
using BenCore.Repositories;

namespace BenCore
{
    class Program
    {
        static async Task Main(string[] args)
        {
            DependencyContainer container = new DependencyContainer();

            container.Register<IUsuarioRepository, UsuarioRepository>();

            BenCoreHost host = new BenCoreHost(container, 5000);
            await host.StartAsync();
        }
    }
}