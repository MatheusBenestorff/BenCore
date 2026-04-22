using System.Collections.Generic;
using BenCore.Mvc;
using Torff.Ttp;
namespace BenCore.Controllers
{
    public class UsuarioController : BenController
    {
        [HttpGet("/api/usuarios")]
        public TtpResponse ListarTodos()
        {
            var lista = new List<object>
            {
                new { id = 1, nome = "Matheus Benestorff", cargo = "Arquiteto de Software" },
                new { id = 2, nome = "Linus Torvalds", cargo = "Desenvolvedor" }
            };

            return Ok(lista);
        }

        [HttpPost("/api/usuarios")]
        public TtpResponse CriarUsuario()
        {
            return Ok(new { mensagem = "Usuário criado com sucesso no BenCore!" });
        }
    }
}