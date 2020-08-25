using Samantha.Test.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Samantha.Test.Classes
{
    public class Person : IPerson
    {

        public string Name { get; set; }


        public Person()
        {
            Name = ((new Random()).NextDouble() * 10000).ToString();
        }
    }
}
