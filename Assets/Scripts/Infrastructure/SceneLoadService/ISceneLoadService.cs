using System;

namespace Game.Infrastructure.SceneLoadService
{
    public interface ISceneLoadService
    {
        void Load(string name, Action onLoaded);
    }
}