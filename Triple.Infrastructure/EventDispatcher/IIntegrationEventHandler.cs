using System.Threading.Tasks;

namespace Triple.Infrastructure.EventDispatcher
{
    public interface IIntegrationEventHandler<T>
    {
        Task HandleAsync(T @event);
    }
}
