using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    /// <summary>
    /// 事件的订阅池，用于注册事件
    /// </summary>
    public class Notice
    {
        public void SendNotice()
        {
            B b = new B();//注册A规范下B的事件
            C c = new C();//注册A规范下C的事件

        }
    }
}
