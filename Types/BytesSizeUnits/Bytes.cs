namespace Diabase.StrongTypes.Units
{
    public class Bytes : BytesValue
    {
        public Bytes() : base(0M, bytes)
        {
        }

        public Bytes(decimal value) : base(value, bytes)
        {
        }

        public Bytes(BytesValue value) : base(value, bytes)
        {
        }

        public static implicit operator Bytes(decimal value) => new(value);
    }
}
