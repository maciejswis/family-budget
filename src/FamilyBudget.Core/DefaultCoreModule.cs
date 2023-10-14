using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace FamilyBudget.Core;

/// <summary>
/// An Autofac module that is responsible for wiring up services defined in the Core project.
/// </summary>
public class DefaultCoreModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var coreAssembly = Assembly.GetExecutingAssembly();
        // Scan an assembly for services
        builder.RegisterAssemblyTypes(coreAssembly)
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces();
    }
}

