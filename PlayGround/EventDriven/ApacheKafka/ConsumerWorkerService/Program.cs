using Confluent.Kafka;
using ConsumerWorkerService;

var builder = Host.CreateApplicationBuilder(args);

var consumerConfig = builder.Configuration.GetSection("MessageBroker").Get<ConsumerConfig>();
var consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
consumer.Subscribe("public-all");

builder.Services.AddSingleton(consumer);
builder.Services.AddHostedService<ConsumerWorker>();

var host = builder.Build();
host.Run();
