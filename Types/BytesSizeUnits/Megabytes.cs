namespace Diabase.StrongTypes.Units
{
    public class Megabytes : BytesValue
    {
        public Megabytes() : base(0M, megabytes)
        {
        }

        public Megabytes(decimal value) : base(value, megabytes)
        {
        }

        public Megabytes(BytesValue value) : base(value, megabytes)
        {
        }

        public static implicit operator Megabytes(decimal value) => new(value);
    }
}

