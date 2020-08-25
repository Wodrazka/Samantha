using System;
using System.Collections.Generic;
using System.Text;

namespace Sandbox
{
    public class UserData
    {
        int _randomValue;
        IUser _user;

        public UserData(IUser user)
        {
            _user = user;
            _randomValue = (int)((new Random()).NextDouble() * 100);
        }

        public string GetUsername()
        {
            return _user.Name;
        }

        public string GetNickname()
        {
            return "Granerkbrand" + _randomValue;
        }

    }
}
