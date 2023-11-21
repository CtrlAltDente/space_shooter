using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SpaceShooter.Network
{
    public class SessionsList : MonoBehaviour
    {
        [SerializeField]
        private SessionButton _sessionButtonPrefab;
        [SerializeField]
        private List<SessionButton> _sessions = new List<SessionButton>();
        [SerializeField]
        private Transform _listTransform;

        [SerializeField]
        private List<SessionInfo> _findedSessions = new List<SessionInfo>();

        public void ClearAllSessions()
        {
            foreach (SessionButton session in _sessions)
            {
                Destroy(session.gameObject);
            }

            _sessions.Clear();
        }

        public void ProceedReceivedBytes(byte[] dataBytes)
        {
            try
            {
                string jsonData = Encoding.UTF8.GetString(dataBytes);
                SessionInfo sessionInfo = JsonUtility.FromJson<SessionInfo>(jsonData);

                Debug.Log($"Session: Id:{sessionInfo.SessionId}, Ip:{sessionInfo.SessionIp}");

                if (CheckDisabledSession(sessionInfo))
                {
                    _findedSessions.Remove(_findedSessions.Find(session => session.SessionId == sessionInfo.SessionId));
                }
                else if (CheckDuplication(sessionInfo))
                {
                    _findedSessions.Add(sessionInfo);
                }

                UpdateSessions();
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        private bool CheckDisabledSession(SessionInfo sessionInfo)
        {
            if (sessionInfo.SessionIp == "0.0.0.0")
            {
                return true;
            }

            return false;
        }

        private bool CheckDuplication(SessionInfo sessionInfo)
        {
            foreach (SessionInfo session in _findedSessions)
            {
                if (session.SessionIp.Equals(sessionInfo.SessionIp))
                {
                    return false;
                }
            }

            return true;
        }

        private void UpdateSessions()
        {
            ClearAllSessions();

            foreach (SessionInfo session in _findedSessions)
            {
                SessionButton sessionButton = Instantiate(_sessionButtonPrefab, _listTransform);
                _sessions.Add(sessionButton);
                Debug.Log("Session button added");
                sessionButton.SetData(session.SessionId, session.SessionIp);
            }
        }
    }
}