namespace SampleEphReceiver
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Azure.EventHubs;
    using Microsoft.Azure.EventHubs.Processor;

    public class Program
    {
        private const string EventHubConnectionString = "Endpoint=sb://ak-event-namespace.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=kD6fR1EJiJZ5bPwIu44l6TsrfYKn7daYayuVrm8oZLw=";
        private const string EventHubName = "eventhub-1";
        private const string StorageContainerName = "ak-container";
        private const string StorageAccountName = "akstorage123";
        private const string StorageAccountKey = "SH6bq1DGvC39lFJz2Iqo4lSbqT37WY6jHEYCkCq3xI3jBUGbJfdKQqALjT4MfZ9UZi0a+h9VbWfxvK+JpXjlgA==";

        private static readonly string StorageConnectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", StorageAccountName, StorageAccountKey);

        public static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string[] args)
        {
            Console.WriteLine("Registering EventProcessor...");

            var eventProcessorHost = new EventProcessorHost(
                EventHubName,
                PartitionReceiver.DefaultConsumerGroupName,
                EventHubConnectionString,
                StorageConnectionString,
                StorageContainerName);

            // Registers the Event Processor Host and starts receiving messages
            await eventProcessorHost.RegisterEventProcessorAsync<SimpleEventProcessor>();

            Console.WriteLine("Receiving. Press enter key to stop worker.");
            Console.ReadLine();

            // Disposes of the Event Processor Host
            await eventProcessorHost.UnregisterEventProcessorAsync();
        }
    }
}