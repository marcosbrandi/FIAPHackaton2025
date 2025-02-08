using HM.Core.Utils;

namespace HM.Clientes.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus")).AddHostedService<NovoContatoIntegrationHandler>();
            //services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus")).AddHostedService<AtualizaContatoIntegrationHandler>();
            //services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus")).AddHostedService<DeleteContatoIntegrationHandler>();
        }
    }
}