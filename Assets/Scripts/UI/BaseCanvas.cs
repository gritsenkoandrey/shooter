using UnityEngine;

namespace Game.UI
{
    public sealed class BaseCanvas : MonoBehaviour
    {
        [SerializeField] private RectTransform _root;
        public RectTransform Root => _root;
    }
}