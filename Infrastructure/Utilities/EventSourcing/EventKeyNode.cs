using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallyTech.Infrastructure.EventSourcing
{
    internal class EventKeyNode : EventRootNode
    {
        protected override bool IsAddAllEvent
        {
            get { return true; }
        }

        protected override EventNode CreateEventNode()
        {
            return new EventConsumerNode();
        }

        protected override string GetKey(IConsumer consumer)
        {
            return consumer.Key;
        }

        protected override string GetKey<T>(IEventData<T> eventData)
        {
            return eventData.Key;
        }
    }
}