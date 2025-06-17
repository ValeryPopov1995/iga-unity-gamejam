using Zenject;

namespace AncestralPotatoes.Character
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PotatoInventory>().FromComponentInChildren();
            Container.Bind<PlayerHand>().FromComponentInChildren();
            Container.Bind<PlayerAnimator>().FromComponentInChildren();
        }
    }
}