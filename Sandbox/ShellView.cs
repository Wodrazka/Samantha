using Samantha;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sandbox
{
    public class ShellView
    {

        private UserView _userView;

        public ShellView(IContainer container, UserView userView, Generic<ShellView> generic)
        {
            container.Resolve<ItemData>().GetItems();
            Console.WriteLine(container.Resolve<UserData>().GetNickname());
            Console.WriteLine(container.Resolve<UserData>().GetNickname());
            Console.WriteLine(container.Resolve<UserData>().GetNickname());
            Console.WriteLine(container.Resolve<UserData>().GetNickname());
            _userView = userView;

            Console.WriteLine($"ShellView constructed with: {generic.Type.FullName}");
        }

        public void Start()
        {
            Console.WriteLine("ShellView - Start");
            _userView.Start();
        }

    }
}
