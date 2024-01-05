using SpaceShooter.GameLogic;
using SpaceShooter.Network;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SpaceShooter.UI
{
    public class InGameMenu : MonoBehaviour
    {
        [SerializeField]
        private InputActionReference _openMenuAction;
        [SerializeField]
        private InputActionReference _exitFromSessionAction;

        [SerializeField]
        private GameObject _menuObject;

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
            if(!_menuObject.activeSelf)
            {
                SetActiveMenu(true);
            }
        }

        private void CloseMenu(InputAction.CallbackContext callbackContext)
        {
            if(_menuObject.activeSelf)
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
            _menuObject.SetActive(isActive);

            if(isActive)
            {
                _menuAutoClose = StartCoroutine(AutoDisableMenu());
            }
            else
            {
                StopCoroutine(_menuAutoClose);
                _menuAutoClose = null;
            }
        }

        private IEnumerator AutoDisableMenu()
        {
            yield return new WaitForSeconds(3);
            SetActiveMenu(false);
        }
    }
}