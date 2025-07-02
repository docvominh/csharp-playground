using Marten;
using Marten.Events.Daemon;
using Marten.Events.Daemon.Coordination;
using MediatR;

namespace BankAccountWithMarten.Cqrs.Command;

public record RebuildProjectionCommand(string ProjectionType) : IRequest;

internal class RebuildProjectionCommandHandler : IRequestHandler<RebuildProjectionCommand>
{
    private readonly IDocumentStore _store;
    private readonly IProjectionCoordinator _projectionCoordinator;

    public RebuildProjectionCommandHandler(IDocumentStore store, IProjectionCoordinator projectionCoordinator)
    {
        _store = store;
        _projectionCoordinator = projectionCoordinator;
    }

    public Task Handle(RebuildProjectionCommand request, CancellationToken cancellationToken)
    {
        Type type = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .FirstOrDefault(x => x.Name == request.ProjectionType)
            ?? throw new InvalidOperationException();


        IProjectionDaemon? daemon = _projectionCoordinator.DaemonForMainDatabase();

        return daemon.RebuildProjectionAsync(type, cancellationToken);
    }
}
