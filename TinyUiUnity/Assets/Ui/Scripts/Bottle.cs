using System.Threading.Tasks;
using Bottle.Scripts;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Scripts
{
    public class Bottle : MonoBehaviour
    {
        [SerializeField] private Image _emptyBottleViewPrefab;

        public BottleData BottleData { get; private set; }

        private Image _view;
        private RectTransform _rectTransform;
        private Animator _animator;

        private void Awake()
        {
            _rectTransform = (RectTransform)transform;
        }

        public void Init(BottleData bottleData, float transparency, float scale)
        {
            BottleData = bottleData;

            ClearChildren();

            var bottleView = BottleData.IsAvailable ? BottleData.BottleViewPrefab : _emptyBottleViewPrefab;
            _view = Instantiate(bottleView, transform);
            _view.rectTransform.localScale = Vector3.one * scale;
            SetTransparency(transparency);

            _animator = _view.GetComponent<Animator>();
            PauseAnimation();
        }

        private void ClearChildren()
        {
            foreach (Transform t in transform)
            {
                Destroy(t.gameObject);
            }
        }

        private void SetTransparency(float transparency)
        {
            _view.color = new Color(_view.color.r, _view.color.g, _view.color.b, transparency);
        }

        public async UniTask MoveObjectAsync(Vector3 targetPosition, float targetTransparency, float targetScale, float duration)
        {
            var startPosition = _rectTransform.localPosition;
            var direction = targetPosition - startPosition;

            var startAlpha = _view.color.a;
            var alphaDir = targetTransparency - startAlpha;

            var startScale = _view.rectTransform.localScale;
            var scaleDir = Vector3.one * targetScale - startScale;

            var t = 0f;
            while (t < duration)
            {
                var progress = Mathf.Clamp01(t / duration);
                _rectTransform.localPosition = startPosition + direction * progress;
                SetTransparency(startAlpha + alphaDir * progress);
                _view.rectTransform.localScale = startScale + scaleDir * progress;
                t += Time.deltaTime;
                await Task.Yield();
            }

            _view.rectTransform.localScale = Vector3.one * targetScale;
            SetTransparency(targetTransparency);
            _rectTransform.localPosition = targetPosition;
        }

        public void PlayAnimation()
        {
            if (_animator == null)
            {
                return;
            }

            _animator.speed = 1f;
        }

        public void PauseAnimation()
        {
            if (_animator == null)
            {
                return;
            }

            _animator.speed = 0f;
        }
    }
}