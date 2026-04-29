using System.Collections.Generic;
using BenCore.Mvc;
using BenCore.Repositories;
using Torff.Ttp;
namespace BenCore.Controllers
{
    public class UsuarioController : BenController
    {
        private readonly IUsuarioRepository _repo;

        public UsuarioController(IUsuarioRepository repo)
        {
            _repo = repo;
        }
        
        [HttpGet("/api/usuarios")]
        public TtpResponse ListarTodos()
        {
            var dados = _repo.BuscarNomesNoBanco();
            return Ok(dados);
        }

        [HttpPost("/api/usuarios")]
        public TtpResponse CriarUsuario()
        {
            return Ok(new { mensagem = "Usuário criado com sucesso no BenCore!" });
        }
    }
}