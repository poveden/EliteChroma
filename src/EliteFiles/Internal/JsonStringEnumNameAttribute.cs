namespace EliteFiles.Internal
{
    [AttributeUsage(AttributeTargets.Field)]
    internal sealed class JsonStringEnumNameAttribute : Attribute
    {
        public JsonStringEnumNameAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
