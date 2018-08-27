# easyInject
An easy to use dependency injection library for C# specifically targeted with Unity compatibility in mind

# Overview
This library aims to be a non-instrusive dependency injection solution for C#. Non-intrsuive in the way that you don't need to change a component in order for it to be injectable or have it's dependencies solved.

easyInject achieves this by not requiring a class to have it's members marked down using Attributes ( [Inject] ) in order to be able to construct it or solve it's dependencies. In fact, a BindingContext, easyInject's dependency container, only maps types to functions that return that type, it care's not how such functions go in order to conjure an object of that type to be returned. 

This approach to dependency injection brings code using easyInject much closer to "regular" object oriented coded in the sense that functions returning the objects for the container can be seen sort like a constructor, so avoiding some DI pitfalls and helping to bridge the learning gap to those new to DI.

# Examples
There are two main namespaces in easyInject, namely ```EasyInject.IOC and EasyInject.IOC.extensions```, both are important in order to compile this examples.

## Manual Binding
Manual binding is a type of binding where you programmatically define bindings, it's dependencies and what the return value is. It allows for some interesting kinds of architectures and it's not better or wrost then it's sibling, automatic binding, just differente. Some types of bidings can only be defined in manual binding.

### Simple Value binding
Binding a value is simple like that
```csharp
...
IBindingContext context = TestsFactory.BindingContext();

int value = 45;

context.Bind<int>().To(() => 45);

Console.WriteLine(context.Get<int>());
...
```
output
```
45
```

Or you can bind directly to the value instead of a function

```csharp
...
IBindingContext context = TestsFactory.BindingContext();

int value = 45;

context.Bind<int>().To(45);

Console.WriteLine(context.Get<int>());
...
```
Output:
```
45
```
### Giving names to your bindings
You can have multiple bindings for the same type, as long as they have different names

```csharp
...
IBindingContext context = TestsFactory.BindingContext();

int valFoo = 45;
int valBar = 33;

context.Bind<int>("foo").To(valFoo);
context.Bind<int>("bar").To(valBar);

Console.WriteLine(context.Get<int>("foo"));
Console.WriteLine(context.Get<int>("bar"));
...
```
Output:
```
45
33
```

### Empty is a name
A value binded to no name is actualy binded to a especial name, ```InnerBindingNames.Empty```, so...

```csharp
...
IBindingContext context = TestsFactory.BindingContext();

int value = 45;

context.Bind<int>().To(45);

Console.WriteLine(context.Get<int>());
Console.WriteLine(context.Get<int>(InnerBindingNames.Empty));
...
```

Output:
```
45
45
```
### Specifying dependencies
And here is the main job of a DI library, to fetch dependencies for you

```csharp
...
IBindingContext context = TestsFactory.BindingContext();

context.Bind<string>().To("Hello world!");

context.Bind<int>().With<string>().To((strParam)=> strParam.Length);

Console.WriteLine(context.Get<int>());
...
```
Output:
```
12
```

And of course you can get dependencies with a specific name
```csharp
...
IBindingContext context = TestsFactory.BindingContext();

context.Bind<string>().To("Hello world!");
context.Bind<string>("Planet").To("Hello planet!");

context.Bind<int>().With<string>("Planet").To((strParam)=> strParam.Length);

Console.WriteLine(context.Get<int>());
...
```
Output:
```
13
```
## Unsafe Bidings
# TODO

## Automatic Bindings
# TODO