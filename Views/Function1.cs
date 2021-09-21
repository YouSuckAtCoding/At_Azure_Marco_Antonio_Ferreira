using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Views
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([QueueTrigger("queueviews", Connection = "AzureWebJobsStorage")] string myQueueItem,
            FunctionContext context)
        {
            var logger = context.GetLogger("Function1");
            logger.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
