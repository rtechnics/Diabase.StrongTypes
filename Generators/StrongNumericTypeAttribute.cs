namespace Diabase.StrongTypes
{
    public class StrongNumericTypeAttribute : StrongTypeAttribute
    {
        public NumericConstraint Constraints { get; set; }
        public bool ValidationRequired { get; set; }
    }
}
