using BenCore.ORM.Providers;
using BenCore.ORM.Translation;

namespace BenCore.ORM
{
    public class BenSet<T> where T : class
    {
        private readonly IDbProvider _provider;
        private readonly ISqlTranslator _translator;

        public BenSet(IDbProvider provider, ISqlTranslator translator)
        {
            _provider = provider;
            _translator = translator;
        }

        public async Task<string> InsertAsync(T entity)
        {
            string sql = _translator.GenerateInsert(entity);
            System.Console.WriteLine($"[BenCore.ORM] Generated Query: {sql}");
            return await _provider.ExecuteQueryAsync(sql);
        }
    }
}