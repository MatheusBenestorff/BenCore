namespace BenCore.Repositories
{
    public interface IUsuarioRepository
    {
        List<string> BuscarNomesNoBanco();
    }

    public class UsuarioRepository : IUsuarioRepository
    {
        public List<string> BuscarNomesNoBanco()
        {
            return new List<string> { "Matheus", "Torvalds", "Uncle Bob" };
        }
    }
}