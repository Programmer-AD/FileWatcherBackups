using FileWatcherBackups.CommandHandling.Surface;
using FileWatcherBackups.CommandHandling.Surface.Internals;

namespace FileWatcherBackups.CommandHandling;

internal class CommandHandler : ICommandHandler
{
    private readonly IReadOnlyDictionary<Type, ICommandExecutor> commandExecutorsByRequestTypes;

    public CommandHandler(IEnumerable<ICommandExecutor> commandExecutors)
    {
        commandExecutorsByRequestTypes = commandExecutors.ToDictionary(x => x.RequestType);
    }

    public TResponse Handle<TResponse>(ICommandRequest<TResponse> request)
    {
        var requestType = request.GetType();

        if (commandExecutorsByRequestTypes.TryGetValue(requestType, out var executor))
        {
            const string methodName = nameof(ICommandExecutor<ICommandRequest<object>, object>.Execute);
            var executorType = executor.GetType();
            var executorInterfaceType = typeof(ICommandExecutor<,>).MakeGenericType(requestType, typeof(TResponse));
            var interfaceMap = executorType.GetInterfaceMap(executorInterfaceType);
            var method = interfaceMap.TargetMethods.First(method => method.Name == methodName && method.IsPublic);

            object rawResult = method.Invoke(executor, new[] { request })!;
            var result = (TResponse)rawResult;
            return result;
        }
        else
        {
            throw new NotImplementedException($"Handling of command request with type \"{requestType.FullName}\" is not implemented");
        }
    }
}
