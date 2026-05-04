namespace BenCore.ORM.Translation
{
    public interface ISqlGenerator
    {
        string Generate<T>(T entity = null) where T : class;
    }
}