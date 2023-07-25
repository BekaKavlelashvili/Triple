using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Triple.Infrastructure.EventDispatcher
{
    public class IntegrationEventDispatcher
    {
        private IServiceProvider _serviceProvider;

        public IntegrationEventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task DispatcheAsync<T>(List<T> @events) where T : class
        {
            foreach (var @event in events)
            {
                var type = @event.GetType();
                var eventHandlerType = typeof(IIntegrationEventHandler<>).MakeGenericType(@event.GetType());

                var eventHandlers = _serviceProvider.GetServices(eventHandlerType);
                if (eventHandlers == null || !eventHandlers.Any())
                    throw new Exception($"Event {@event.GetType().Name} DoesNotHaveHanlder");

                foreach (var handler in eventHandlers)
                {
                    await ((dynamic)handler).HandleAsync((dynamic)@event);
                }
            }
        }
    }
}
