using SpaceShooter.GameLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SpaceShooter.Installers
{
    public class SceneLogicInstaller : MonoInstaller
    {
        public ScreenFader ScreenFader;
        public Loadbar Loader;

        public override void InstallBindings()
        {
            Container.BindInstance(ScreenFader).AsSingle().NonLazy();
            Container.BindInstance(Loader).AsSingle().NonLazy();
        }
    }
}