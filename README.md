# Samantha

## Get Started

Quick start:

Register components with a `ContainerBuilder` and then build the component container.

```C#
ContainerBuilder containerBuilder = new ContainerBuilder();

containerBuilder.Register<ItemData>();
containerBuilder.Register<UserData>();

containerBuilder.RegisterAssemplyTypes(Assembly.GetExecutingAssembly())
    .Where(e => e.ConstructionType.Name.EndsWith("View"));

IContainer container = containerBuilder.Build();
```

Resolve services from the container:

```C#
UserData userData = container.Resolve<UserData>();
```
