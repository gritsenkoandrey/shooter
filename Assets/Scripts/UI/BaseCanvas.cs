using UnityEngine;

namespace UI
{
    public sealed class BaseCanvas : MonoBehaviour
    {
        [SerializeField] private RectTransform _root;
        public RectTransform Root => _root;
    }
}