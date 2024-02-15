using SpaceShooter.Network.Sessions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter.Network.Roles
{
    public class ClientLogic : MonoBehaviour
    {
        public UnityEvent OnLocalClientSelected;
        public UnityEvent OnInternetClientSelected;

        [SerializeField]
        private SessionsLocator _sessionsLocator;

        public void SelectLocalClient()
        {
            try
            {
                _sessionsLocator.StartSessionSearch();
                OnLocalClientSelected.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        public void SelectInternetClient()
        {

        }
    }
}