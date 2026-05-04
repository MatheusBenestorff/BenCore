using System.Reflection;

namespace BenCore.ORM.Translation
{
    public class DefaultSqlTranslator : ISqlTranslator
    {
        public string GenerateInsert<T>(T entity)
        {
            Type type = typeof(T);
            string tableName = type.Name + "s"; 

            PropertyInfo[] properties = type.GetProperties();
            var values = properties.Select(p => p.GetValue(entity)?.ToString() ?? "");
            string dataToInsert = string.Join(" - ", values);

            return $"INSERT INTO {tableName} VALUES ('{dataToInsert}')";
        }

        public string GenerateSelect<T>()
        {
            Type type = typeof(T);
            string tableName = type.Name + "s";
            return $"SELECT * FROM {tableName}";
        }
    }
}