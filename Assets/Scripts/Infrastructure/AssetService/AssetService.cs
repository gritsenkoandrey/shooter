using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Infrastructure.AssetService
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class AssetService : IAssetService
    {
        T IAssetService.LoadFromResources<T>(string path) => Resources.Load<T>(path);
        T[] IAssetService.LoadAllFromResources<T>(string path) => Resources.LoadAll<T>(path);
        async UniTaskVoid IAssetService.CleanUp() => await Resources.UnloadUnusedAssets();
    }
}