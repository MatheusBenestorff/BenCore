using System.Reflection;
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

        public async Task<string> InsertAsync<T>(T entity)
        {
            Type type = typeof(T);
            
            string tableName = type.Name + "s"; 

            PropertyInfo[] properties = type.GetProperties();

            var values = properties.Select(p => p.GetValue(entity)?.ToString() ?? "");
            string dataToInsert = string.Join(" - ", values);

            string sqlQuery = $"INSERT INTO {tableName} VALUES ('{dataToInsert}')";

            Console.WriteLine($"[BenCore.ORM] Query Gerada: {sqlQuery}");

            return await _provider.ExecuteQueryAsync(sqlQuery);
        }
    }
}