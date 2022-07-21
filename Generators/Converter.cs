using System;

namespace Diabase.StrongTypes
{
    [Flags]
    public enum Converter
    {
        None = 0,
        JsonConverter = 1,
        TypeConverter = 2,
        ValueConverter = 4,
        Customize = 8,
    }
}
