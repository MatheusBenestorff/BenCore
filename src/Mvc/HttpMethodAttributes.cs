namespace BenCore.Mvc
{
    [AttributeUsage(AttributeTargets.Method)]
    public class HttpGetAttribute : Attribute
    {
        public string Template { get; }

        public HttpGetAttribute(string template)
        {
            Template = template;
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class HttpPostAttribute : Attribute
    {
        public string Template { get; }

        public HttpPostAttribute(string template)
        {
            Template = template;
        }
    }
}