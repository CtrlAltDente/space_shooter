using SpaceShooter.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SpaceShooter.Installers
{
    public class SceneLogicInstaller : MonoInstaller
    {
        public LoadingScreen ScreenFader;

        public override void InstallBindings()
        {
            Container.BindInstance(ScreenFader).AsSingle().NonLazy();
        }
    }
}