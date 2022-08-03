namespace Diabase.StrongTypes.Units
{
    public class Petabytes : BytesValue
    {
        public Petabytes() : base(0M, petabytes)
        {
        }

        public Petabytes(decimal value) : base(value, petabytes)
        {
        }

        public Petabytes(BytesValue value) : base(value, petabytes)
        {
        }

        public static implicit operator Petabytes(decimal value) => new(value);
    }
}

