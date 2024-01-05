using SpaceShooter.GameLogic;
using SpaceShooter.Network;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

namespace SpaceShooter.UI
{
    public class InGameMenu : MonoBehaviour
    {
        [SerializeField]
        private InputActionReference _openMenuAction;
        [SerializeField]
        private InputActionReference _exitFromSessionAction;

        [SerializeField]
        private CanvasGroup _menuObject;

        [SerializeField]
        private NetworkControl _networkControl;
        [SerializeField]
        private SceneLoader _sceneLoader;

        private Coroutine _menuAutoClose;

        private void Start()
        {
            _openMenuAction.action.performed += OpenMenu;
            _exitFromSessionAction.action.performed += ExitFromSession;
        }

        private void OnDestroy()
        {
            _openMenuAction.action.performed -= OpenMenu;
            _exitFromSessionAction.action.performed -= ExitFromSession;
        }

        private void OpenMenu(InputAction.CallbackContext callbackContext)
        {
            if(!_menuObject.gameObject.activeSelf)
            {
                SetActiveMenu(true);
            }
        }

        private void CloseMenu(InputAction.CallbackContext callbackContext)
        {
            if(_menuObject.gameObject.activeSelf)
            {
                SetActiveMenu(false);
            }
        }

        private void ExitFromSession(InputAction.CallbackContext callbackContext)
        {
            CloseMenu(new InputAction.CallbackContext());
            _networkControl.Shutdown(false);
        }

        private void SetActiveMenu(bool isActive)
        {
            if(isActive)
            {
                _menuObject.gameObject.SetActive(isActive);
                _menuObject.DOFade(1, 1).OnComplete(() => _menuAutoClose = StartCoroutine(AutoDisableMenu()));
            }
            else
            {
                _menuObject.DOFade(0, 1).OnComplete(() =>
                {
                    _menuObject.gameObject.SetActive(isActive);
                    StopCoroutine(_menuAutoClose);
                    _menuAutoClose = null;
                });
            }
        }

        private IEnumerator AutoDisableMenu()
        {
            yield return new WaitForSeconds(3);
            SetActiveMenu(false);
        }
    }
}