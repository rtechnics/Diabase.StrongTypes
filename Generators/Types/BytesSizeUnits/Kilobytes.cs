namespace Diabase.StrongTypes.Units
{
    public class Kilobytes : BytesValue
    {
        public Kilobytes() : base(0M, kilobytes)
        {
        }

        public Kilobytes(decimal value) : base(value, kilobytes)
        {
        }

        public Kilobytes(BytesValue value) : base(value, kilobytes)
        {
        }

        public static implicit operator Kilobytes(decimal value) => new(value);
    }
}

