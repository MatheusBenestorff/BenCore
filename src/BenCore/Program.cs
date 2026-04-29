using System;
using System.Threading.Tasks;
using BenCore.ORM;
using BenCore.ORM.Providers;

namespace BenCore
{
    // 1. Nossa classe de teste (O desenvolvedor cria isso)
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
            Console.WriteLine("Iniciando Teste do BenCore.ORM...");

            IDbProvider krakenPlugin = new KrakenProvider("localhost", 5432);
            BenContext db = new BenContext(krakenPlugin);

            Usuario novoUser = new Usuario 
            { 
                Nome = "Linus Torvalds", 
                Email = "linus@linux.org", 
                Idade = 54 
            };

            string resultado = await db.InsertAsync(novoUser);

            Console.WriteLine($"\n[Resposta do Banco]: {resultado}");
            
        }
    }
}