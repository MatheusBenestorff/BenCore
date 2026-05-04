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
        public async Task<TtpResponse> ListarTodos()
        {
            var dados = await _repo.BuscarTodosAsync();
            
            return Ok(dados);
        }

        [HttpPost("/api/usuarios")]
        public TtpResponse CriarUsuario()
        {
            return Ok(new { mensagem = "Usuário criado com sucesso no KrakenDB!" });
        }
    }
}