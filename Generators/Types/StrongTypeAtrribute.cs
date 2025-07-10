using System;

namespace Diabase.StrongTypes.Types
{
    public class StrongTypeAttribute : Attribute
    {
        protected StrongTypeAttribute()
        {
        }

        public Converter Converters { get; set; }
    }
}
