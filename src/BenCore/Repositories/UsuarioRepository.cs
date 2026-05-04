using BenCore.ORM;

namespace BenCore.Repositories
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> BuscarTodosAsync();
    }

    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly BenContext _db;

        public UsuarioRepository(BenContext db)
        {
            _db = db;
        }

        public async Task<List<Usuario>> BuscarTodosAsync()
        {
            return await _db.Set<Usuario>().SelectAsync();
        }
    }

}