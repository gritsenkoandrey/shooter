using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class BaseScreen : MonoBehaviour
    {
        [SerializeField] private protected Button _button;

        public event Action OnClose;

        protected virtual void OnEnable() { }
        protected virtual void OnDisable() { }

        protected void Close() => OnClose?.Invoke();
    }
}