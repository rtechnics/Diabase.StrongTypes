namespace Diabase.StrongTypes.Types
{
    public class StrongIdAttribute : StrongTypeAttribute
    {
        public new Converter Converters { get => base.Converters; set => base.Converters = value; }
    }
}
