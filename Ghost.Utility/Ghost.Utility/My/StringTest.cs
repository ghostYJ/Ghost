using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ghost.Utility.My
{
    public static class StringTest
    {
        public static void TestStringInterned()
        {
            //test1();
        }


        public static void test1()
        {
            string e1 = "lock";
            string ee = e1 + "it";

            string s3 = "it";
            string s4 = "lock" + s3;

            string s5 = "lock";
            string s6 = s5 + "it";

            string testee = "ee:" + getMemory(ee) + "  " + (string.IsNullOrEmpty(string.IsInterned(ee)) ? "No" : "Yes");
            string tests4 = "s4:" + getMemory(s4) + "  " + (string.IsNullOrEmpty(string.IsInterned(s4)) ? "No" : "Yes");
            string tests6 = "s6:" + getMemory(s6) + "  " + (string.IsNullOrEmpty(string.IsInterned(s6)) ? "No" : "Yes");
        }


        public static string getMemory(this object o) // 获取引用类型的内存地址方法  
        {
            GCHandle h = GCHandle.Alloc(o, GCHandleType.Pinned);
            IntPtr addr = h.AddrOfPinnedObject();
            return "0x" + addr.ToString("X");
        }
    }
}


