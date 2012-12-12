using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroTDD_Module4_Library
{
    public class BroadcasterWithEvents
    {
        public event EventHandler NotificationSent;

        protected virtual void OnNotificationSent(EventArgs eventArguments)
        {
            if (null != NotificationSent)
            {
                NotificationSent(this, eventArguments);
            }
        }

        public void NotifyAll()
        {
            OnNotificationSent(EventArgs.Empty);
        }
    }
}
