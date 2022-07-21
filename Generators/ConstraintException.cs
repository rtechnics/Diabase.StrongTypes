using System;

namespace Diabase.StrongTypes
{
    public class ConstraintException : Exception
    {
        public ConstraintException(string message) : base(message)
        {
        }

        public override string Message => base.Message.Replace(PropertyNamePlaceholder, "Value");

        public virtual string CustomMessage(string propertyName) => base.Message.Replace(PropertyNamePlaceholder, propertyName);

        public const string PropertyNamePlaceholder = "{value}";
    }
}
