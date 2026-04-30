using System;
using System.Threading.Tasks;
using BenCore.ORM;
using BenCore.ORM.Providers;

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
            Console.WriteLine("Iniciando Teste do BenCore.ORM...");

            IDbProvider krakenPlugin = new KrakenProvider("localhost", 5432);
            BenContext db = new BenContext(krakenPlugin);

            Usuario novoUser = new Usuario 
            { 
                Nome = "Ada Lovelace", 
                Email = "ada@computing.com", 
                Idade = 36 
            };

            var tabelaUsuarios = db.Set<Usuario>();
            string resultado = await tabelaUsuarios.InsertAsync(novoUser);

            Console.WriteLine($"\n[Resposta do Banco]: {resultado}");
            
        }
    }
}