using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpReflection
{
    public class Source
    {
        public Dictionary<string, object> table;

        public Source()
        {
            table = new Dictionary<string, object>();
            table.Add("Attribute1", "value1");
            table.Add("Attribute2", 234);
        }
    }
    
    public class Destination
    {
        public string Attribute1 { get; set; }
        public int Attribute2 { get; set; }
    }

    [TestClass]
    public class AccessingObjectProperitesTests
    {
        [TestMethod]
        public void ReadingObjectsProperties()
        {

            Destination dest = new Destination();
            Source source = new Source();            

            var properties = dest.GetType().GetProperties().OrderBy(x => x.Name).Select(x => x.Name);

            System.Console.WriteLine("BEFORE:");
            foreach(var property in properties)
            {
                System.Console.WriteLine("{0}:{1}", property, dest.GetType().GetProperty(property).GetValue(dest));

                dest.GetType().GetProperty(property).SetValue(dest, source.table[property.ToString()]);
            }

            System.Console.WriteLine("AFTER:");
            foreach (var property in properties)
            {                
                System.Console.WriteLine("{0}:{1}",property,dest.GetType().GetProperty(property).GetValue(dest));
            }           
        }
    }
}
