using System;

namespace Diabase.StrongTypes
{
    [AttributeUsage(AttributeTargets.Struct, Inherited = false)]
    public class StrongFloatTypeAttribute : StrongNumericTypeAttribute
    {
    }
}
