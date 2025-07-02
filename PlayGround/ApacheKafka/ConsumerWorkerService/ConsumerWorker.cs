using Confluent.Kafka;

namespace ConsumerWorkerService;

public class ConsumerWorker(ILogger<ConsumerWorker> logger, IConsumer<Ignore, string> consumer) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            try
            {
                var consumeResult = consumer.Consume(stoppingToken);
                logger.LogInformation(
                    $"Consumed message '{consumeResult.Message.Value}' at: '{consumeResult.TopicPartitionOffset}'.");
            }
            catch (OperationCanceledException)
            {
                // Ensure the consumer leaves the group cleanly and final offsets are committed.
                consumer.Close();
            }
            catch (ConsumeException e)
            {
                logger.LogError($"Error occured: {e.Error.Reason}");
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}
