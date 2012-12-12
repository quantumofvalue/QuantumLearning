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
    public class BroadcasterTests
    {
        MockRepository mocks;
        Broadcaster broadcaster;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();

            broadcaster = new Broadcaster();
        }

        [TearDown]
        public void TearDown()
        {
            mocks.VerifyAll();
        }

        [Test]
        public void SubscriberReceivesNotificationsFromBroadcaster()
        {
            Subscriber subscriber = mocks.StrictMock<Subscriber>();
            Expect.Call(delegate { subscriber.Listen(); });

            mocks.ReplayAll();

            broadcaster.RegisterSubscriber(subscriber);
            broadcaster.NotifyAll();
        }

        [Test]
        public void TwoSubscribersReceiveNotificationsFromBroadcaster()
        {
            Subscriber subscriber1 = CreateSubscriberMock();
            Subscriber subscriber2 = CreateSubscriberMock();

            mocks.ReplayAll();
            
            broadcaster.RegisterSubscriber(subscriber1);
            broadcaster.RegisterSubscriber(subscriber2);
            broadcaster.NotifyAll();
        }

        private Subscriber CreateSubscriberMock()
        {
            Subscriber subscriber = mocks.StrictMock<Subscriber>();
            Expect.Call(delegate { subscriber.Listen(); });
            return subscriber;
        }

    }
}
