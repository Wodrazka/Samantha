using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection.Test.Classes
{
    public class Person
    {

        public string Name { get; set; }


        public Person()
        {
            Name = ((new Random()).NextDouble() * 10000).ToString();
        }
    }
}
