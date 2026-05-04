using System;
using System.Threading.Tasks;
using BenCore.IoC;
using BenCore.ORM;
using BenCore.ORM.Providers;
using BenCore.Controllers; 
using Torff;
using BenCore.Core;
using BenCore.Repositories;

namespace BenCore
{
    public class Usuario
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public int Idade { get; set; }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("======================================");
            Console.WriteLine(" INITIALING BENCORE WEB FRAMEWORK   ");
            Console.WriteLine("======================================");

            var container = new DependencyContainer();

            IDbProvider krakenPlugin = new KrakenProvider("localhost", 5432);
            container.RegisterInstance<IDbProvider>(krakenPlugin);

            container.Register<BenContext, BenContext>();
            container.Register<IUsuarioRepository, UsuarioRepository>();
            container.Register<UsuarioController, UsuarioController>();

            int port = 5000;
            Console.WriteLine($"[Torff] Waking up the Web Server on port {port}...");
            var server = new BenCoreHost(container,port); 
            await server.StartAsync();
        }
    }
}