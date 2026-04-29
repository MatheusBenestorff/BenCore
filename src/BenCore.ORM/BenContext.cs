using BenCore.ORM.Providers;

namespace BenCore.ORM
{
    public class BenContext
    {
        private readonly IDbProvider _provider;

        public BenContext(IDbProvider provider)
        {
            _provider = provider;
        }

        public async Task<string> ExecuteRawSqlAsync(string sqlQuery)
        {
            return await _provider.ExecuteQueryAsync(sqlQuery);
        }
    }
}