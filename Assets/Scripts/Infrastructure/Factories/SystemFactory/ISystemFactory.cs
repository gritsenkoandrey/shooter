using Core.Implementation;

namespace Infrastructure.Factories.SystemFactory
{
    public interface ISystemFactory
    {
        ISystem[] CreateGameSystems();
    }
}