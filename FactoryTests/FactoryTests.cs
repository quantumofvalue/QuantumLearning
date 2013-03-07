using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FactoryTests
{
    public interface IClassA
    {
    }

    public class ClassA : IClassA
    { 
    }

    public class ClassB : IClassA
    {
    }

    public class Factory
    {
        static public IClassA create(String className)
        {
            if ("ClassA" == className)
            {
                return new ClassA();
            }
            else if ("ClassB" == className)
            {
                return new ClassB();
            }

            return null;
        }

        static public IClassA createClassA()
        {
            return new ClassA();
        }

        static public IClassA createClassB()
        {
            return new ClassB();
        }
    }

    [TestClass]
    public class FactoryTests
    {
        [TestMethod]
        public void TestFactoryCreatesInstanceOfClassA()
        {         
            Assert.IsInstanceOfType(Factory.create("ClassA"), typeof(ClassA));
        }

        [TestMethod]
        public void TestFactoryCreatesInstanceOfClassB()
        {
            Assert.IsInstanceOfType(Factory.create("ClassB"), typeof(ClassB));
        }
    }
}
