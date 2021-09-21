using System;
using System.Data.SqlClient;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Models;

namespace FunctionApp1
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([QueueTrigger("queueviews", Connection = "AzureWebJobsStorage")] Guitarrista guitarrista,ILogger log )
        {
            log.LogInformation($"C# Queue trigger function processed: {guitarrista.Nome}");
            var connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var textSql = $@"UPDATE [dbo].[Guitarrista] SET [NumeroDeViews] = [NumeroDeViews] + 1 WHERE [Id] = {guitarrista.Id}";

                using (SqlCommand cmd = new SqlCommand(textSql, conn))
                {
                    var rowsAffected = cmd.ExecuteNonQuery();
                    log.LogInformation($"rowsAffected: {rowsAffected}");
                }
            }
        }
    }
}
