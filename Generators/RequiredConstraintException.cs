using System;
using System.Collections.Generic;
using System.Text;

namespace Diabase.StrongTypes
{
    public class RequiredConstraintException : ConstraintException
    {
        public RequiredConstraintException() : base($"{PropertyNamePlaceholder} is required.")
        {
        }    
    }
}
