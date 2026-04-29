namespace BenCore.ORM.Providers
{
    public interface IDbProvider
    {
        Task<string> ExecuteQueryAsync(string sql);
    }
}