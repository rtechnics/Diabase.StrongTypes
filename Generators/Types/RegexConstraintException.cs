namespace Diabase.StrongTypes.Types
{
    public class RegexConstraintException : ConstraintException
    {
        public RegexConstraintException() : base($"{PropertyNamePlaceholder} is not in the proper format.")
        {
        }
    }
}
