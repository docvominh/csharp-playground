using BankAccountWithMarten.EventSource;
using BankAccountWithMarten.EventSource.Events;
using Marten;
using Marten.Events.Daemon.Resiliency;
using Marten.Events.Projections;
using Weasel.Core;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("Postgres")!;

builder.Services.AddMarten(
        options =>
        {
            options.Connection(connectionString);

            options.Events.AddEventType<AccountCreate>();
            options.Events.AddEventType<AccountUpdate>();
            options.Events.AddEventType<DepositEvent>();
            options.Events.AddEventType<WithdrawEvent>();

            options.Projections.Snapshot<AccountAggregate>(SnapshotLifecycle.Inline);

            options.Projections.Add<AccountHistoryProjection>(ProjectionLifecycle.Inline);
            
            if (builder.Environment.IsDevelopment())
            {
                options.AutoCreateSchemaObjects = AutoCreate.All;
            }
        }
    )
    .AddAsyncDaemon(DaemonMode.Solo);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
