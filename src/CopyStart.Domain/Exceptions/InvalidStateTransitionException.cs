namespace CopyStart.Domain.Exceptions;

public class InvalidStateTransitionException : DomainException
{
    public string CurrentState { get; }
    public string TargetState { get; }

    public InvalidStateTransitionException(string entityName, string currentState, string targetState)
        : base($"Cannot transition {entityName} from state '{currentState}' to state '{targetState}'.")
    {
        CurrentState = currentState;
        TargetState = targetState;
    }
}
