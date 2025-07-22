namespace Diabase.StrongTypes.Units
{
    public class BytesUnit
    {
        public decimal BaseMultiplier { get; private set; }
        public string Suffix { get; private set; }

        public BytesUnit(decimal baseMultiplier, string suffix)
        {
            BaseMultiplier = baseMultiplier;
            Suffix = suffix;
        }

        public static bool operator ==(BytesUnit a, BytesUnit b)
        {
            return a.BaseMultiplier == b.BaseMultiplier;
        }

        public static bool operator !=(BytesUnit a, BytesUnit b)
        {
            return a.BaseMultiplier != b.BaseMultiplier;
        }

        public override bool Equals(object obj)
        {
            return obj is BytesUnit unit &&
                   BaseMultiplier == unit.BaseMultiplier;
        }

        public override int GetHashCode()
        {
            return 136214327 + BaseMultiplier.GetHashCode();
        }
    }
}
