using System.Collections.Generic;
using System.Linq;
using Bottle.Scripts;
using Cysharp.Threading.Tasks;
using Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Ui.Scripts
{
    public class CharacterSelectionScreen : MonoBehaviour
    {
        [SerializeField] private RectTransform _bottlesContainer;
        [SerializeField] private RectTransform _bottlesHolder;
        [SerializeField] private Bottle _baseBottlePrefab;
        [SerializeField] private SideNavigation _sideNavigation;
        [SerializeField] private Button _selectButton;
        [SerializeField] private Transform _emptyInDemoLabel;

        [Header("Bottle Info Refs")]
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private Image _icon;
        [SerializeField] private RectTransform _iconHolder;

        [Header("Visual Settings")]
        [SerializeField] private int _bottlesAmount = 7;
        [SerializeField] private float _heightMult = 0.5f;
        [SerializeField] private AnimationCurve _transparencyCurve;
        [SerializeField] private Vector2 _scaleMinMax;
        [SerializeField] private float _switchDuration = 1f;

        [Inject] private readonly BottleService _bottleService;

        private readonly Vector3[] _corners = new Vector3[4];
        private readonly List<Bottle> _bottles = new();
        private float _containerWidth;
        private float _containerHeight;
        private float _gap;
        private int HalfIndex => Mathf.FloorToInt(_bottlesAmount / 2f);

        private void Awake()
        {
            _sideNavigation.SetupCallbacks(OnSwitchRightClicked, OnSwitchLeftClicked);
        }

        private void OnSwitchLeftClicked()
        {
            SwitchBottle(-1);
        }

        private void OnSwitchRightClicked()
        {
            SwitchBottle(1);
        }

        private async void SwitchBottle(int direction)
        {
            _sideNavigation.Disable();

            var nextPositions = _bottles.Select(x => x.transform.localPosition).ToList();

            var previousList = _bottles.ToList();
            var switchIndex = 0;
            for (var i = 0; i <= _bottlesAmount; i++)
            {
                var to = switchIndex.Clamp(_bottlesAmount);
                var from = (switchIndex + direction).Clamp(_bottlesAmount);
                _bottles[to] = previousList[from];
                switchIndex += direction;
                _bottles[to].PauseAnimation();
            }

            if (direction < 0)
            {
                var bottleData = _bottleService.GetPrevBottleData(previousList[0].BottleData);
                _bottles[0].Init(bottleData, 0f, _scaleMinMax.x);
            }
            else if (direction > 0)
            {
                var bottleData = _bottleService.GetNextBottleData(previousList[_bottlesAmount - 1].BottleData);
                _bottles[_bottlesAmount - 1].Init(bottleData, 0f, _scaleMinMax.x);
            }

            var moveTasks = new List<UniTask>();

            for (var i = 0; i < _bottlesAmount; ++i)
            {
                moveTasks.Add(_bottles[i].MoveObjectAsync(nextPositions[i], GetBottleTransparency(i), GetBottleScale(i), _switchDuration));
            }

            await UniTask.WhenAll(moveTasks);
            _sideNavigation.Enable();

            SelectMiddleBottle();
        }

        private void SelectMiddleBottle()
        {
            var bottle = _bottles[HalfIndex];
            bottle.PlayAnimation();
            var bottleData = bottle.BottleData;

            _name.text = bottleData.Title;
            _name.color = _name.color.SetAlpha(bottleData.IsAvailable ? 1f : 0.3f);

            _description.text = bottleData.GetDescription();
            _description.color = _description.color.SetAlpha(bottleData.IsAvailable ? 1f : 0.3f);

            _icon.sprite = bottleData.Icon;
            _iconHolder.gameObject.SetActive(bottleData.IsAvailable);

            _selectButton.interactable = bottleData.IsAvailable;

            _emptyInDemoLabel.gameObject.SetActive(!bottleData.IsAvailable);
        }

        private void Start()
        {
            _bottlesContainer.GetLocalCorners(_corners);
            _containerWidth = _bottlesContainer.sizeDelta.x;
            _containerHeight = _bottlesContainer.sizeDelta.y / 2f;
            _gap = _containerWidth / _bottlesAmount;

            GenerateBottles();
            SelectMiddleBottle();
        }

        private void GenerateBottles()
        {
            for (var i = 0; i < _bottlesAmount; ++i)
            {
                var xPos = GetPosX(i);
                var position = GetBottlePosition(xPos);
                var b = Instantiate(_baseBottlePrefab, _bottlesHolder);
                ((RectTransform)b.transform).localPosition = position;

                var bottleData = _bottleService.Bottles[i % _bottleService.Bottles.Count];
                b.Init(bottleData, GetBottleTransparency(i), GetBottleScale(i));
                _bottles.Add(b);
            }
        }

        private float GetBottleTransparency(int index)
        {
            var indexTo01 = MapIndexTo01(index);
            return _transparencyCurve.Evaluate(indexTo01);
        }

        // Map index to 0-1 where 1 is middle index and 0 is edge indices
        private float MapIndexTo01(int index)
        {
            return 1f - Mathf.Clamp01(Mathf.Abs(index - HalfIndex) / (float)HalfIndex);
        }

        private float GetBottleScale(int index)
        {
            var indexTo01 = MapIndexTo01(index);
            return _scaleMinMax.x + indexTo01 * (_scaleMinMax.y - _scaleMinMax.x);
        }

        private float GetPosX(int index)
        {
            return _gap * index + _gap / 2f;
        }

        private Vector3 GetBottlePosition(float xPos)
        {
            return _corners[0] +
                   Vector3.right * xPos +
                   Vector3.up * GetBottlePosY(xPos);
        }

        private float GetBottlePosY(float xPos)
        {
            var progress = Mathf.Clamp01(xPos / _containerWidth);
            return (_containerHeight - _containerHeight * Mathf.Sin(progress * Mathf.PI)) * _heightMult;
        }
    }
}
