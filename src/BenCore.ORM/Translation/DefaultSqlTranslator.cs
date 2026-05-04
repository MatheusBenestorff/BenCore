namespace BenCore.ORM.Translation
{
    public class DefaultSqlTranslator : ISqlTranslator
    {
        private readonly InsertGenerator _insertGenerator = new InsertGenerator();
        private readonly SelectGenerator _selectGenerator = new SelectGenerator();

        public string GenerateInsert<T>(T entity) where T : class
        {
            return _insertGenerator.Generate(entity);
        }

        public string GenerateSelect<T>() where T : class
        {
            return _selectGenerator.Generate<T>();
        }
    }
}