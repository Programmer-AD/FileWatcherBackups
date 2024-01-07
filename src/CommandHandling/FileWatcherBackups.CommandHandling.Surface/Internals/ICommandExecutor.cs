namespace FileWatcherBackups.CommandHandling.Surface.Internals;

public interface ICommandExecutor<TRequest, TResponse> : ICommandExecutor
    where TRequest : ICommandRequest<TResponse>
{
    Type ICommandExecutor.RequestType => typeof(TRequest);

    TResponse Execute(TRequest request);
}

public interface ICommandExecutor
{
    Type RequestType { get; }
}
