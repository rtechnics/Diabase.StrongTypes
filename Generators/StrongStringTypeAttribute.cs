using System;

namespace Diabase.StrongTypes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class StrongStringTypeAttribute : StrongTypeAttribute
    {
        public ImplicitNullConversionMode ImplicitNullConversionMode { get; set; }
        public StringConstraint Constraints { get; set; }
        public bool ValidationRequired { get; set; }
        public bool IncludeEmptyValue { get; set; }
    }
}
