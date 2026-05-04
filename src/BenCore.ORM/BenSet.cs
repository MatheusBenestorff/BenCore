using System.Reflection;
using System.Text.Json;
using BenCore.ORM.Models;
using BenCore.ORM.Providers;
using BenCore.ORM.Translation;

namespace BenCore.ORM
{
    public class BenSet<T> where T : class
    {
        private readonly IDbProvider _provider;
        private readonly ISqlTranslator _translator;

        public BenSet(IDbProvider provider, ISqlTranslator translator)
        {
            _provider = provider;
            _translator = translator;
        }

        public async Task<string> InsertAsync(T entity)
        {
            string sql = _translator.GenerateInsert(entity);
            System.Console.WriteLine($"[BenCore.ORM] Generated Query: {sql}");
            return await _provider.ExecuteQueryAsync(sql);
        }

        public async Task<List<T>> SelectAsync()
        {
            string sql = _translator.GenerateSelect<T>();
            Console.WriteLine($"[BenCore.ORM] Generated Query: {sql}");
            
            string jsonResponse = await _provider.ExecuteQueryAsync(sql);

            var krakenResult = JsonSerializer.Deserialize<KrakenResponse>(jsonResponse);
            var listaDeObjetos = new List<T>();

            if (krakenResult != null && krakenResult.Success && krakenResult.Data != null)
            {
                PropertyInfo[] properties = typeof(T).GetProperties();

                foreach (string linha in krakenResult.Data)
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
                                Console.WriteLine($"[BenCore.ORM] Warning: Error converting value '{valores[i]}'");
                            }
                        }
                    }

                    listaDeObjetos.Add(obj);
                }
            }

            return listaDeObjetos;
        }
    }
}