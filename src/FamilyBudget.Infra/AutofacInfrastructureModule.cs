using System;
using System.Reflection;
using Autofac;
using FamilyBudget.Core;
using FamilyBudget.Core.Entities;

using Module = Autofac.Module;

namespace FamilyBudget.Infra;

public class AutofacInfrastructureModule: Module
{
    private readonly List<Assembly> _assemblies = new List<Assembly>();

    public AutofacInfrastructureModule(bool isDevelopment, Assembly? callingAssembly = null)
    {
        AddToAssembliesIfNotNull(callingAssembly);
    }

    private void AddToAssembliesIfNotNull(Assembly? assembly)
    {
        if (assembly != null)
        {
            _assemblies.Add(assembly);
        }
    }

    private void LoadAssemblies()
    {
        // TODO: Replace these types with any type in the appropriate assembly/project
        var coreAssembly = Assembly.GetAssembly(typeof(Budget));
        var infrastructureAssembly = Assembly.GetAssembly(typeof(AutofacInfrastructureModule));

        AddToAssembliesIfNotNull(coreAssembly);
        AddToAssembliesIfNotNull(infrastructureAssembly);
    }

    protected override void Load(ContainerBuilder builder)
    {
        LoadAssemblies();
        RegisterEF(builder);
    }

    private void RegisterEF(ContainerBuilder builder)
    {
        //builder.RegisterGeneric(typeof(IRepository<>))
        //  .InstancePerLifetimeScope();

        var infraAssembly = Assembly.GetExecutingAssembly();
        // Scan an assembly for services
        builder.RegisterAssemblyTypes(infraAssembly)
               .Where(t => t.Name.EndsWith("Repository"))
               .AsImplementedInterfaces();
    }
}

