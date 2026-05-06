using System.Text.Json;
using BenCore.ORM.Mapping;
using BenCore.ORM.Models;
using BenCore.ORM.Providers;
using BenCore.ORM.Translation;

namespace BenCore.ORM
{
    public class BenSet<T> where T : class
    {
        private readonly IDbProvider _provider;
        private readonly ISqlTranslator _translator;
        private readonly EntityMapper _mapper;

        public BenSet(IDbProvider provider, ISqlTranslator translator)
        {
            _provider = provider;
            _translator = translator;
            _mapper = new EntityMapper();
        }

        public async Task<string> InsertAsync(T entity)
        {
            string sql = _translator.GenerateInsert(entity);
            System.Console.WriteLine($"[BenCore.ORM] Generated Query: {sql}");
            return await _provider.ExecuteQueryAsync(sql);
        }

        public async Task<List<T>> SelectAsync()
        {
            string sql = _translator.GenerateSelect<T>();
            Console.WriteLine($"[BenCore.ORM] Generated Query: {sql}");
            string jsonResponse = await _provider.ExecuteQueryAsync(sql);

            var krakenResult = JsonSerializer.Deserialize<KrakenResponse>(jsonResponse);

            if (krakenResult != null && krakenResult.Success && krakenResult.Data != null)
            {
                return _mapper.MapToEntities<T>(krakenResult.Data);
            }

            return new List<T>();
        }
        
        public async Task<string> DeleteAsync(T entity)
        {
            string sql = _translator.GenerateDelete(entity);
            System.Console.WriteLine($"[BenCore.ORM] Generated Query: {sql}");
            return await _provider.ExecuteQueryAsync(sql);
        }
    }
}