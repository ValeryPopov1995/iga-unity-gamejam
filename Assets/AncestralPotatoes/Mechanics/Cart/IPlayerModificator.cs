namespace AncestralPotatoes.Cart
{
    public interface IPlayerModificator
    {
        public float MoveCoef { get; }
        public float JumpCoef { get; }
        public float RotateCoef { get; }
    }
}