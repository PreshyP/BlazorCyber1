namespace BlazorCyber
{
    public interface IDependecyA
    {
        void DoSomething();
    }
}

// DependencyAImplementation.cs
namespace Example
{
    public class DependencyAImplementation : IDependencyA
    {
        public void DoSomething()
        {
            // Implementation code
        }
    }
}

// MyService.cs
namespace Example
{
    public class MyService
    {
        private readonly IDependencyA _dependencyA;

        public MyService(IDependencyA dependencyA)
        {
            _dependencyA = dependencyA;
        }

        public void Execute()
        {
            //_dependencyA.DoSomething();
        }
    }
}
   

