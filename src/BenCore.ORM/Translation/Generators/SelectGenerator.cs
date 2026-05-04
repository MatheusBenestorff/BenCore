namespace BenCore.ORM.Translation
{
    public class SelectGenerator : ISqlGenerator
    {
        public string Generate<T>(T entity = null) where T : class
        {
            string tableName = typeof(T).Name + "s";
            return $"SELECT * FROM {tableName}";
        }
    }
}