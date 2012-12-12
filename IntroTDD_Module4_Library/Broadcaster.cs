using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroTDD_Module4_Library
{
    public class Broadcaster
    {
        private List<Subscriber> _subscribers = new List<Subscriber>();

        public void NotifyAll()
        {
            foreach (Subscriber subscriber in _subscribers)
            {
                subscriber.Listen();
            }
        }

        public void RegisterSubscriber(Subscriber subscriber)
        {
            _subscribers.Add(subscriber);
        }
    }
}
