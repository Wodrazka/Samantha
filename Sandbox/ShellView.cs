using DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sandbox
{
    public class ShellView
    {

        private UserView _userView;

        public ShellView(IContainer container, UserView userView)
        {
            container.Resolve<ItemData>().GetItems();
            _userView = userView;
        }

        public void Start()
        {
            Console.WriteLine("Shell View - Start");
            _userView.Start();
        }

    }
}
