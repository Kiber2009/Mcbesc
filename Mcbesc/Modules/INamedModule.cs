namespace Mcbesc.Modules
{
    internal interface INamedModule : IModule
    {
        string Name { get; }
    }
}