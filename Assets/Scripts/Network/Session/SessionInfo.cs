using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Network.Sessions
{
    [Serializable]
    public struct SessionInfo
    {
        public int SessionId;
        public string SessionIp;

        public SessionInfo(int sessionId, string sessionIp)
        {
            SessionId = sessionId;
            SessionIp = sessionIp;
        }
    }
}