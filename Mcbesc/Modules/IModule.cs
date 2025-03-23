namespace Mcbesc.Modules
{
    internal interface IModule
    {
        string Description { get; }
        string[] Help { get; }

        void Execute(string[] args);
    }
}