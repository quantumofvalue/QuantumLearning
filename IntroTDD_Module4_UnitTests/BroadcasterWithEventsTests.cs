using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using Rhino.Mocks;

using IntroTDD_Module4_Library;

namespace IntroTDD_Module4
{
    [TestFixture]
    class BroadcasterWithEventsTests
    {
        MockRepository mocks;
        BroadcasterWithEvents broadcaster;
        EventSubscriber subscriber1, subscriber2;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();

            broadcaster = new BroadcasterWithEvents();
        }

        [TearDown]
        public void TearDown()
        {
            mocks.VerifyAll();
        }

        [Test]
        public void SubscriberReceivesNotificationsFromBroadcasterWithEvents()
        {
            EventSubscriber subscriber = mocks.StrictMock<EventSubscriber>();
            Expect.Call(delegate { subscriber.Listen(broadcaster, EventArgs.Empty); });

            mocks.ReplayAll();

            broadcaster.NotificationSent += subscriber.Listen;
            broadcaster.NotifyAll();
        }

        [Test]
        public void TwoSubscribersReceiveNotificationsFromBroadcasterWithEvents()
        {
            CreateTwoSubscriberMocks();

            mocks.ReplayAll();

            StartListeningToBroadcast();
            NotifyAllSubscribers();
        }

        private void NotifyAllSubscribers()
        {
            broadcaster.NotifyAll();
        }

        private void StartListeningToBroadcast()
        {
            broadcaster.NotificationSent += subscriber1.Listen;
            broadcaster.NotificationSent += subscriber2.Listen;
        }

        private void CreateTwoSubscriberMocks()
        {
            subscriber1 = CreateEventSubscriberMock();
            subscriber2 = CreateEventSubscriberMock();
        }

        private EventSubscriber CreateEventSubscriberMock()
        {
            EventSubscriber subscriber = mocks.StrictMock<EventSubscriber>();
            Expect.Call(delegate { subscriber.Listen(broadcaster, EventArgs.Empty); });
            return subscriber;
        }
    }
}
