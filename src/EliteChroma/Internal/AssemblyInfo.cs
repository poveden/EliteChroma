using System.Reflection;

namespace EliteChroma.Internal
{
    internal sealed class AssemblyInfo
    {
        public AssemblyInfo()
        {
            var a = Assembly.GetExecutingAssembly();

            Title = a.GetCustomAttribute<AssemblyTitleAttribute>()!.Title;
            Version = a.GetName().Version!;
            Description = a.GetCustomAttribute<AssemblyDescriptionAttribute>()!.Description;
            Product = a.GetCustomAttribute<AssemblyProductAttribute>()!.Product;
            Copyright = a.GetCustomAttribute<AssemblyCopyrightAttribute>()!.Copyright;
            Company = a.GetCustomAttribute<AssemblyCompanyAttribute>()!.Company;
        }

        public string Title { get; }

        public Version Version { get; }

        public string Description { get; }

        public string Product { get; }

        public string Copyright { get; }

        public string Company { get; }
    }
}
