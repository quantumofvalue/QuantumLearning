using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using Rhino.Mocks;

namespace IntroTDD_Module4
{

    public interface ExampleInterface
    {
        void MethodWithoutArgumnets();
        int MethodWithoutArgumentsButReturningAValue();
        int MethodWithTwoArguments(int argument1, object argument2);
    }

    [TestFixture]
    public class RhinoMock
    {
        MockRepository mocks;
        ExampleInterface mock;

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            
        }

        [SetUp]
        public void Setup()
        {
            mocks = new MockRepository();
            mock = mocks.StrictMock<ExampleInterface>();
        }

        [TearDown]
        public void TearDown()
        {
            mocks.VerifyAll();
        }

        [Test]
        public void SimpleExpectations()
        {
            Expect.Call(delegate { mock.MethodWithoutArgumnets(); });

            mocks.ReplayAll();

            mock.MethodWithoutArgumnets();
        }

        [Test]
        public void ExpectationsOnAMethodReturningAValue()
        {
            Expect.Call(mock.MethodWithoutArgumentsButReturningAValue()).Return(253);

            mocks.ReplayAll();

            Assert.AreEqual(253, mock.MethodWithoutArgumentsButReturningAValue());
        }

        [Test]
        public void ExpectingConcreteMethodArguments()
        {
            Expect.Call(mock.MethodWithTwoArguments(0, null)).Constraints(Rhino.Mocks.Constraints.Is.Equal(3), Rhino.Mocks.Constraints.Is.NotNull()).Return(25);

            mocks.ReplayAll();

            Assert.AreEqual(25,mock.MethodWithTwoArguments(3,new Object()));
        }

        [Test]
        public void IgnoringSelectedArgument()
        {
            Expect.Call(mock.MethodWithTwoArguments(0, null)).Constraints(Rhino.Mocks.Constraints.Is.Anything(), Rhino.Mocks.Constraints.Is.Null()).Return(25);

            mocks.ReplayAll();

            Assert.AreEqual(25, mock.MethodWithTwoArguments(24, null));
        }

        [Test]
        public void IgnoringAllArguments()
        {
            Expect.Call(mock.MethodWithTwoArguments(0, null)).IgnoreArguments().Return(23);

            mocks.ReplayAll();

            Assert.AreEqual(23, mock.MethodWithTwoArguments(1, 2));
        }
    }
}
