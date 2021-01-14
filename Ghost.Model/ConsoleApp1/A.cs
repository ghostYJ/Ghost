using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    /// <summary>
    /// 余总
    /// </summary>
    public sealed class A
    {
        private static readonly A instance = null;
        static A()
        {
            instance = new A();
        }
        private A()
        {
        }
        public static A Instance
        {
            get
            {
                return instance;
            }
        }
        public delegate void RaiseEventHandler(string hand);
        public delegate void FallEventHandler();

        /// <summary>
        /// 首领A举杯事件
        /// </summary>
        public event RaiseEventHandler RaiseEvent;
        /// <summary>
        /// 首领A摔杯事件
        /// </summary>
        public event FallEventHandler FallEvent;

        /// <summary>
        /// 举杯
        /// </summary>
        /// <param name="hand">手：左、右</param>
        public void Raise(string hand)
        {
            //Console.WriteLine("首领A{0}手举杯", hand);
            if (RaiseEvent != null)
                RaiseEvent(hand);
        }
        /// <summary>
        /// 摔杯
        /// </summary>
        public void Fall()
        {
            //Console.WriteLine("首领A摔杯");
            if (FallEvent != null)
                FallEvent();
        }
    }
}
