using System;

namespace Diabase.StrongTypes.Types
{
    [Flags]
    public enum StringConstraint
    {
        None = 0,
        Required = 1,
        Regex = 2,
        Custom = 4,
    }
}
