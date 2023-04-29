namespace DataSynchronization.Configuration.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigSectionAttribute : Attribute
    {
        public string Name;
        public ConfigSectionAttribute(string name)
        {
            Name = name;
        }
    }
}
