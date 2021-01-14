using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    /// <summary>
    /// 戴金涛
    /// </summary>
    public class C
    {
        A a=A.Instance;
        public C()
        {
            a.RaiseEvent += a_RaiseEvent;
            a.FallEvent += a_FallEvent;
        }

        public void a_RaiseEvent(string hand)
        {
            if (hand == "右手")
                Attack();
        }

        public void a_FallEvent()
        {
            FallAttack();
        }

        public void Attack()
        {
            Console.WriteLine("部下C发布公告内容:'你奶奶喊你回家洗澡'！");
        }

        public void FallAttack()
        {
            Console.WriteLine("协同攻击:部下B发起发布公告内容:'天气不怎么样'！");
        }
    }
}
