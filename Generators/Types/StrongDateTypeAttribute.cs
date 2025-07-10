using System;

namespace Diabase.StrongTypes.Types
{
    [AttributeUsage(AttributeTargets.Struct, Inherited = false)]
    public class StrongDateTypeAttribute : StrongNumericTypeAttribute
    {
    }
}
