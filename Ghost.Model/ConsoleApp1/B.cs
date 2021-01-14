using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    /// <summary>
    /// 尹俊
    /// </summary>
    public class B
    {
        A a=A.Instance;
        public B()
        {           
            a.RaiseEvent += a_RaiseEvent;
            a.FallEvent += a_FallEvent;
        }

        public void a_RaiseEvent(string hand)
        {
            if (hand == "左手")
                Attack();
        }

        public void a_FallEvent()
        {
            FallAttack();
        }

        public void Attack()
        {
            Console.WriteLine("部下B发布公告内容:'你妈喊你回家吃饭'！");
        }

        public void FallAttack()
        {
            Console.WriteLine("协同发布:部下B发起发布公告内容:'天气晴朗'！");
        }
    }
}
