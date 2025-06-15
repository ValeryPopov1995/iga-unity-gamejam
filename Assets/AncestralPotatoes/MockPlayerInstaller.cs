using UnityEngine;
using Zenject;

namespace AncestralPotatoes
{
    public class MockPlayerInstaller : MonoInstaller
    {
        [SerializeField] private MockPlayerLocator _playerLocator;

        public override void InstallBindings()
        {
            Container.Bind<IPlayerLocator>().To<MockPlayerLocator>().FromInstance(_playerLocator).AsSingle();
        }
    }
}