using JetBrains.Annotations;
using UnityEngine;

namespace Game.Infrastructure.InputService
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class InputService : IInputService
    {
        private bool _isEnable;
        
        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }
        
        void IInputService.Enable(bool isEnable) => _isEnable = isEnable;

        void IInputService.Execute()
        {
            if (_isEnable == false)
            {
                return;
            }
            
            Horizontal = Input.GetAxis("Horizontal");
            Vertical = Input.GetAxis("Vertical");
        }
    }
}