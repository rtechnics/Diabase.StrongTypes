namespace Diabase.StrongTypes.Units
{
    public class Gigabytes : BytesValue
    {
        public Gigabytes() : base(0M, gigabytes)
        {
        }

        public Gigabytes(decimal value) : base(value, gigabytes)
        {
        }

        public Gigabytes(BytesValue value) : base(value, gigabytes)
        {
        }

        public static implicit operator Gigabytes(decimal value) => new(value);
    }
}

