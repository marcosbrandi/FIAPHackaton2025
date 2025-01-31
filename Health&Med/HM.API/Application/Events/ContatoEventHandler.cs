using MediatR;

namespace HM.Clientes.API.Application.Events
{
    public class ContatoEventHandler : INotificationHandler<ContatoRegistradoEvent>
    {
        public Task Handle(ContatoRegistradoEvent notification, CancellationToken cancellationToken)
        {
            // Enviar evento de confirmação
            return Task.CompletedTask;
        }
    }
}