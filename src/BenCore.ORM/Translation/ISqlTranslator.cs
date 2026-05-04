namespace BenCore.ORM.Translation
{
    public interface ISqlTranslator
    {
        string GenerateInsert<T>(T entity);
        string GenerateSelect<T>();
    }
}