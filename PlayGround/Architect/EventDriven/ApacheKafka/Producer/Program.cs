using Confluent.Kafka;

var config = new ProducerConfig { BootstrapServers = "localhost:9093" };

// If serializers are not specified, default serializers from
// `Confluent.Kafka.Serializers` will be automatically used where
// available. Note: by default strings are encoded as UTF8.
using var p = new ProducerBuilder<Null, string>(config).Build();
try
{
    var dr = await p.ProduceAsync("public-all", new Message<Null, string> { Value = "ha ha ha ha" });
    Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
}
catch (ProduceException<Null, string> e)
{
    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
}
