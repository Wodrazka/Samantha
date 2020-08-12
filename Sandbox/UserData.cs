using System;
using System.Collections.Generic;
using System.Text;

namespace Sandbox
{
    public class UserData
    {
        int randomValue;
        public UserData()
        {
            randomValue = (int)((new Random()).NextDouble() * 100);
        }

        public string GetUserName()
        {
            return "Julian";
        }

        public string GetNickname()
        {
            return "Granerkbrand" + randomValue;
        }

    }
}
