namespace Diabase.StrongTypes.Units
{
    public class Terabytes : BytesValue
    {
        public Terabytes() : base(0M, terabytes)
        {
        }

        public Terabytes(decimal value) : base(value, terabytes)
        {
        }

        public Terabytes(BytesValue value) : base(value, terabytes)
        {
        }

        public static implicit operator Terabytes(decimal value) => new(value);
    }
}

