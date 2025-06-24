using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using fnSBRentProcess;
using Azure.Messaging.ServiceBus;




namespace ProcessoLocacao
{
    public class ProcessoLocacao
    {
        private readonly ILogger<ProcessoLocacao> _logger;
        private readonly IConfiguration _configuration;

        public ProcessoLocacao(ILogger<ProcessoLocacao> logger, IConfiguration configuration)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration;
            
        }

        [Function("ProcessoLocacao")]
        public async Task Run([ServiceBusTrigger("fila-locacao-auto", Connection = "ServiceBusConnection")]
                ServiceBusReceivedMessage message,
                ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            var body = message.Body.ToString();
            _logger.LogInformation("Message Body: {body}", body);
            _logger.LogInformation("Message Content Type: {contentType}", message.ContentType);

            RentModel rentModel = null;
            try
            {
                rentModel = JsonSerializer.Deserialize<RentModel>(body, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (rentModel is null)
                {
                    _logger.LogError("Failed to deserialize message body to RentModel.");
                    await messageActions.DeadLetterMessageAsync(message);
                    return;
                }

                var ConnectionString = _configuration.GetConnectionString("SqlConnectionString");
                using var Connection = new SqlConnection(ConnectionString);
                await Connection.OpenAsync();
                var comandado = new SqlCommand(@"INSERT INTO Rent (Nome, Email, Modelo, Ano, TempoAluguel, Data) VALUES (@Nome, @Email, @Modelo, @Ano, @TempoAluguel, @Data)", Connection);
                comandado.Parameters.AddWithValue("@Nome", rentModel.Nome);
                comandado.Parameters.AddWithValue("@Email", rentModel.Email);
                comandado.Parameters.AddWithValue("@Modelo", rentModel.Modelo);
                comandado.Parameters.AddWithValue("@Ano", rentModel.Ano);
                comandado.Parameters.AddWithValue("@TempoAluguel", rentModel.TempoAluguel);
                comandado.Parameters.AddWithValue("@Data", rentModel.Data);

                var rowsAffected = await comandado.ExecuteNonQueryAsync();

                await messageActions.CompleteMessageAsync(message);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the message.");
                await messageActions.DeadLetterMessageAsync(message);
                return;
            }

            
            
        }
    }
}
