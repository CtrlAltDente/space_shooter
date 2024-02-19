using SpaceShooter.GameLogic.Session;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter.Interfaces
{
    public interface IGameSessionLogic
    {
        public void StartGameSession(SessionConfig sessionConfig);
    }
}
