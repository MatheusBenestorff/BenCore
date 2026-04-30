using System.Threading.Tasks;
using BenCore.ORM.Providers;
using BenCore.ORM.Translation;

namespace BenCore.ORM
{
    public class BenContext
    {
        private readonly IDbProvider _provider;
        private readonly ISqlTranslator _translator;

        public BenContext(IDbProvider provider)
        {
            _provider = provider;
            
            _translator = new DefaultSqlTranslator(); 
        }

        public async Task<string> ExecuteRawSqlAsync(string sqlQuery)
        {
            return await _provider.ExecuteQueryAsync(sqlQuery);
        }

        public BenSet<T> Set<T>() where T : class
        {
            return new BenSet<T>(_provider, _translator);
        }
    }
}