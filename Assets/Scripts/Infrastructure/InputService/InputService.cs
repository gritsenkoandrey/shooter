using JetBrains.Annotations;
using UnityEngine;

namespace Game.Infrastructure.InputService
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class InputService : IInputService
    {
        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }
        
        void IInputService.Execute()
        {
            Horizontal = Input.GetAxis("Horizontal");
            Vertical = Input.GetAxis("Vertical");
        }
    }
}