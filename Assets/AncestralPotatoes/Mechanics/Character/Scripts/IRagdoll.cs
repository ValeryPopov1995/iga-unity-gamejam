using Cysharp.Threading.Tasks;

namespace AncestralPotatoes.Character
{
    public interface IRagdoll
    {
        public bool IsRagdoll { get; }
        public UniTask SetRagdoll(bool state);
    }
}