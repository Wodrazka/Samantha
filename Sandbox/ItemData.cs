using System;
using System.Collections.Generic;
using System.Text;

namespace Sandbox
{
    public class ItemData
    {

        public IEnumerable<string> GetItems()
        {
            return new List<string>()
            {
                "Item1",
                "Item2",
                "Item3",
                "Item4",
                "Item5",
                "Item6",
                "Item7",
            };
        }

    }
}
