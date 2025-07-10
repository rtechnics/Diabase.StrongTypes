using System;

namespace Diabase.StrongTypes.Types
{
    [Flags]
    public enum NumericConstraint
    {
        None = 0,
        MinimumValue = 1,
        MaximumValue = 2,
        Custom = 4
    }
}
