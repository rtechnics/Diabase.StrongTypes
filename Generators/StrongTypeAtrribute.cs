using System;

namespace Diabase.StrongTypes
{
    public class StrongTypeAttribute : Attribute
    {
        protected StrongTypeAttribute()
        {
        }

        public Converter Converters { get; set; }
    }
}
