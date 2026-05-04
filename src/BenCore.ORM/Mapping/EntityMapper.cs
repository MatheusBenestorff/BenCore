using System.Reflection;

namespace BenCore.ORM.Mapping
{
    public class EntityMapper
    {
        public List<T> MapToEntities<T>(string[] dataRows) where T : class
        {
            var listaDeObjetos = new List<T>();
            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (string linha in dataRows)
            {
                string[] valores = linha.Split(new[] { " - " }, StringSplitOptions.None);
                T obj = Activator.CreateInstance<T>();

                for (int i = 0; i < properties.Length; i++)
                {
                    if (i < valores.Length)
                    {
                        try
                        {
                            object valorConvertido = Convert.ChangeType(valores[i], properties[i].PropertyType);
                            properties[i].SetValue(obj, valorConvertido);
                        }
                        catch
                        {
                            Console.WriteLine($"[BenCore.ORM.Mapper] Warning: Error converting value '{valores[i]}'");
                        }
                    }
                }
                listaDeObjetos.Add(obj);
            }

            return listaDeObjetos;
        }
    }
}