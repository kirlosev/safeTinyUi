using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Scripts
{
    public class SideNavigation : MonoBehaviour
    {
        [SerializeField] private Button _rightButton;
        [SerializeField] private Button _leftButton;

        public void SetupCallbacks(Action rightButtonClick, Action leftButtonClick)
        {
            if (rightButtonClick != null)
            {
                _rightButton.onClick.AddListener(rightButtonClick.Invoke);
            }

            if (leftButtonClick != null)
            {
                _leftButton.onClick.AddListener(leftButtonClick.Invoke);
            }
        }

        public void Enable()
        {
            _rightButton.interactable = true;
            _leftButton.interactable = true;
        }

        public void Disable()
        {
            _rightButton.interactable = false;
            _leftButton.interactable = false;
        }
    }
}
