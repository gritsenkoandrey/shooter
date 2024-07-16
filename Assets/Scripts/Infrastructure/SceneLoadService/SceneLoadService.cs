using System;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;

namespace Infrastructure.SceneLoadService
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class SceneLoadService : ISceneLoadService
    {
        void ISceneLoadService.Load(string name, Action onLoaded)
        {
            LoadScene(name, onLoaded).Forget();
        }
        
        private async UniTaskVoid LoadScene(string name, Action onLoaded)
        {
            if (SceneManager.GetActiveScene().name.Equals(name))
            {
                onLoaded?.Invoke();
                
                return;
            }
            
            await SceneManager.LoadSceneAsync(name).ToUniTask();
            
            onLoaded?.Invoke();
        }
    }
}