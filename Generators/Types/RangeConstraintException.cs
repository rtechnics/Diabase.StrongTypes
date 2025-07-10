namespace Diabase.StrongTypes.Types
{
    public class RangeConstraintException : ConstraintException
    {
        public static RangeConstraintException AboveLimit(object value) => new($"{PropertyNamePlaceholder} exceeds upper limit of {value}");
        public static RangeConstraintException BelowLimit(object value) => new($"{PropertyNamePlaceholder} exceeds lower limit of {value}");

        RangeConstraintException(string message) : base(message)
        {
        }
    }
}
