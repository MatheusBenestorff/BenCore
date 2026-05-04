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

            var tabelaUsuarios = db.Set<Usuario>();

            Console.WriteLine("Gravando novo usuário...");
            await tabelaUsuarios.InsertAsync(new Usuario { Nome = "Alan Turing", Email = "alan@enigma.com", Idade = 41 });
            await tabelaUsuarios.InsertAsync(new Usuario { Nome = "Grace Hopper", Email = "grace@navy.mil", Idade = 85 });
            
            Console.WriteLine("\nBuscando todos os usuários do KrakenDB...");
            List<Usuario> usuariosDoBanco = await tabelaUsuarios.SelectAsync();

            Console.WriteLine("\n--- DADOS RETORNADOS E MAPEADOS COM SUCESSO ---");
            foreach (var u in usuariosDoBanco)
            {
                Console.WriteLine($"Nome: {u.Nome} | E-mail: {u.Email} | Idade: {u.Idade} anos");
            }
        }
    }
}