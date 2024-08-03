using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Infrastructure.LoadingScreenService
{
    public sealed class LoadingScreenService : MonoBehaviour, ILoadingScreenService
    {
        [SerializeField] private Image _fillImage;

        private readonly CancellationTokenSource _cancellationTokenSource = new();
        
        private bool _isShow;

        private void OnDestroy()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }

        void ILoadingScreenService.Show()
        {
            if (_isShow)
            {
                return;
            }

            gameObject.SetActive(true);
            _isShow = true;
            ShowAnimation().Forget();
        }

        void ILoadingScreenService.Hide()
        {
            _isShow = false;
        }

        private async UniTaskVoid ShowAnimation()
        {
            try
            {
                float fillAmount = 0f;
                
                _fillImage.fillAmount = fillAmount;
                
                while (_isShow)
                {
                    await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken: _cancellationTokenSource.Token);

                    float speed = Mathematics.Remap(0.9f, 0f, 0f, 1f, fillAmount);

                    fillAmount += speed * Time.deltaTime;
                    
                    _fillImage.fillAmount = Mathf.Clamp01(fillAmount);
                }
                
                while (fillAmount < 1f)
                {
                    await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken: _cancellationTokenSource.Token);
                    
                    float speed = Mathematics.Remap(1f, 0f, 1f, 2.5f, fillAmount);
                    
                    fillAmount += speed * Time.deltaTime;
                    
                    _fillImage.fillAmount = Mathf.Clamp01(fillAmount);
                }

                await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken: _cancellationTokenSource.Token);
                
                gameObject.SetActive(false);
            }
            catch (OperationCanceledException exception)
            {
                Debug.Log(exception.Message);
            }
        }
    }
}