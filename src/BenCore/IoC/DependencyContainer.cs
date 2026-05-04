namespace BenCore.IoC
{
    public class DependencyContainer
    {
        private readonly Dictionary<Type, Type> _dependencies = new();
        
        private readonly Dictionary<Type, object> _instances = new();

        public void Register<TInterface, TImplementation>()
        {
            _dependencies[typeof(TInterface)] = typeof(TImplementation);
        }

        public void RegisterInstance<TInterface>(TInterface instance)
        {
            _instances[typeof(TInterface)] = instance;
        }

        public object Resolve(Type typeToResolve)
        {
            if (_instances.ContainsKey(typeToResolve))
            {
                return _instances[typeToResolve];
            }
            
            Type targetType = _dependencies.ContainsKey(typeToResolve) ? _dependencies[typeToResolve] : typeToResolve;

            var constructor = targetType.GetConstructors().FirstOrDefault();

            if (constructor == null || constructor.GetParameters().Length == 0)
            {
                return Activator.CreateInstance(targetType);
            }

            var parameters = constructor.GetParameters();
            var parameterInstances = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                parameterInstances[i] = Resolve(parameters[i].ParameterType);
            }

            return Activator.CreateInstance(targetType, parameterInstances);
        }
    }
}