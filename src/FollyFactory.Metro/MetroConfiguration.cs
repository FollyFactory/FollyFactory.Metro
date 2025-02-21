using System.Reflection;

namespace FollyFactory.Metro;

public class MetroConfiguration
{
    public Assembly[] AssembliesToScan { get; set; } = Array.Empty<Assembly>();
}
