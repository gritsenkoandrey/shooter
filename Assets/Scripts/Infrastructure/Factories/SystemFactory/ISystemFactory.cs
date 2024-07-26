using Game.Core.Implementation;

namespace Game.Infrastructure.Factories.SystemFactory
{
    public interface ISystemFactory
    {
        ISystem[] CreateGameSystems();
    }
}