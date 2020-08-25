using System;
using System.Collections.Generic;
using System.Text;

namespace Sandbox
{
    public class UserView
    {

        private UserData _userData;

        public UserView(UserData userData)
        {
            _userData = userData;
        }

        public void Start()
        {
            Console.WriteLine(_userData.GetUsername());
        }

    }
}
