using System.Reflection; 

namespace BenCore.Mvc
{
    public class RouteScanner
    {
        public Dictionary<string, MethodInfo> Routes { get; private set; } = new();

        public void Scan()
        {
            Console.WriteLine("[BenCore] Starting the Reflection Scanner...");

            Assembly myProject = Assembly.GetExecutingAssembly();

            var controllerTypes = myProject.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(BenController)) && !t.IsAbstract);

            foreach (var controller in controllerTypes)
            {
                var methods = controller.GetMethods();

                foreach (var method in methods)
                {
                    // [HttpGet]
                    var getAttribute = method.GetCustomAttribute<HttpGetAttribute>();
                    if (getAttribute != null)
                    {
                        string routeKey = $"GET|{getAttribute.Template}";
                        
                        Routes.Add(routeKey, method);
                        Console.WriteLine($"[Scanner] Route mapped: {routeKey} -> {controller.Name}.{method.Name}()");
                    }

                    // [HttpPost]
                    var postAttribute = method.GetCustomAttribute<HttpPostAttribute>();
                    if (postAttribute != null)
                    {
                        string routeKey = $"POST|{postAttribute.Template}";
                        Routes.Add(routeKey, method);
                        Console.WriteLine($"[Scanner] Route mapped: {routeKey} -> {controller.Name}.{method.Name}()");
                    }
                }
            }
        }
    }
}