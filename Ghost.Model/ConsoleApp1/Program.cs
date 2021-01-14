using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //例如此处为点击某个攻击按钮之后，A发出指令
            A a=A.Instance;
            Notice notice = new Notice();
            /**
             * 1.此方法为订阅A的指令去发布公告。
             * 2.此处不关注SendNotice方法具体是哪些人在做哪些操作。
             * 3.未来的人也可以继续在SendNotice方法中去执行某些操作，只要符合A的规范(即发送公告)。
             */

            /**
             * 余总（A类）:1、开放notice类  2、发布指令
             * 虾兵蟹将（B、C类 .....类）: 补充notice类，发布不同的事件。
             */
            notice.SendNotice();

            a.Raise("左手");

            a.Raise("右手");

            a.Fall();
            Console.ReadLine();
        }
    }
}
