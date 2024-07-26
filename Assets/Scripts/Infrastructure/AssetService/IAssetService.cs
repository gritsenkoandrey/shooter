using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Infrastructure.AssetService
{
    public interface IAssetService
    {
        T LoadFromResources<T>(string path) where T : Object;
        T[] LoadAllFromResources<T>(string path) where T : Object;
        UniTaskVoid CleanUp();
    }
}