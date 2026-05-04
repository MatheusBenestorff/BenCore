namespace BenCore.ORM.Translation
{
    public interface ISqlTranslator
    {
        string GenerateInsert<T>(T entity) where T : class;
        string GenerateSelect<T>() where T : class;
    }
}