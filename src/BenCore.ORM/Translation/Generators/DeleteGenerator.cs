using System;
using System.Linq;
using System.Reflection;
using BenCore.ORM.Attributes;

namespace BenCore.ORM.Translation
{
    public class DeleteGenerator : ISqlGenerator
    {
        public string Generate<T>(T entity) where T : class
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            Type type = typeof(T);
            string tableName = type.Name + "s";

            PropertyInfo pkProp = type.GetProperties().FirstOrDefault(p => 
                Attribute.IsDefined(p, typeof(PrimaryKeyAttribute)));

            if (pkProp == null)
            {
                throw new Exception($"[BenCore.ORM] The class {type.Name} does not have the [PrimaryKey] attribute. Unable to generate the WHERE clause.");
            }

            string pkName = pkProp.Name;
            object pkValue = pkProp.GetValue(entity);

            return $"DELETE FROM {tableName} WHERE {pkName} = '{pkValue}'";
        }
    }
}