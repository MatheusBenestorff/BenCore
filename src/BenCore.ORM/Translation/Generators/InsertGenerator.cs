using System.Reflection;

namespace BenCore.ORM.Translation
{
    public class InsertGenerator : ISqlGenerator
    {
        public string Generate<T>(T entity = null) where T : class
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            Type type = typeof(T);
            string tableName = type.Name + "s"; 

            PropertyInfo[] properties = type.GetProperties();
            var values = properties.Select(p => p.GetValue(entity)?.ToString() ?? "");
            string dataToInsert = string.Join(" - ", values);

            return $"INSERT INTO {tableName} VALUES ('{dataToInsert}')";
        }
    }
}