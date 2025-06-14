using UnityEngine;
using Zenject;

namespace AncestralPotatoes.Enemies.Debug
{
    public class DebugEnemyControllerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Camera>().FromComponentInHierarchy().AsSingle();
        }
    }
}