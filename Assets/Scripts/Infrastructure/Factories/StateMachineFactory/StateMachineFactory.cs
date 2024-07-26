using Game.Infrastructure.GameStateMachine;
using Game.Utils;
using JetBrains.Annotations;
using VContainer;

namespace Game.Infrastructure.Factories.StateMachineFactory
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class StateMachineFactory : IStateMachineFactory
    {
        private readonly IObjectResolver _objectResolver;

        public StateMachineFactory(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }

        IGameStateMachine IStateMachineFactory.CreateGameStateMachine()
        {
            GameStateMachine.GameStateMachine gameStateMachine = new GameStateMachine.GameStateMachine();
            gameStateMachine.States.Values.Foreach(_objectResolver.Inject);
            return gameStateMachine;
        }
    }
}