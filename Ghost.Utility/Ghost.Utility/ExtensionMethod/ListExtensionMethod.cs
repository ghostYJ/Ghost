using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ghost.Utility
{
    /// <summary>
    /// List扩展方法
    /// </summary>
    public static class ListExtensionMethod
    {
        /// <summary>
        /// 公开枚举数转成字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="separator">分隔符</param>
        /// <param name="isRepeat">是否在字符串两端添加分隔符</param>
        /// <param name="isIncludeEmpty">是否包含空字符串</param>
        /// <returns></returns>
        public static string JoinToString<T>(this IEnumerable<T> list, string separator, bool isRepeat = false, bool isIncludeEmpty = false)
        {
            //使用IEnumerable<T>公开枚举数而不使用IList<T>是因为在System.Collections.Generic 命名空间(此命名空间包含定义“泛型集合”的接口和类)内的所有“结构”（包含List<T>结构，除了KeyValuePair<TKey, TValue>定义可设置或检索的键/值对以外）都继承了IEnumerable<T>公开枚举数接口，都实现了Enumerator枚举器，可枚举泛型集合。
            if (list == null || list.Count() == 0)
                return string.Empty;

            StringBuilder sb = new StringBuilder();

            foreach (var item in list)
                if (isIncludeEmpty || !string.IsNullOrEmpty(item.ToString()))
                    sb.Append(item.ToString() + separator);

            if (sb.Length > 0)
            {
                if (isRepeat)
                    sb.Insert(0, separator);
                else
                    sb.Remove(sb.Length - separator.Length, separator.Length);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 将StringList转换成IntList
        /// </summary>
        /// <param name="strList"></param>
        /// <returns></returns>
        public static IList<int> ToInt32(this IList<string> strList)
        {
            IList<int> intList = new List<int>();
            if (strList == null || strList.Count() == 0)
                return null;
            foreach (var item in strList)
            {
                int i;
                if (int.TryParse(item.Trim(), out i))
                    intList.Add(i);
            }
            return intList;
        }

        /// <summary>
        /// 在当前List后追加List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="appendList">追加的List</param>
        /// <param name="isRepeat">是否可重复（默认为true）</param>
        public static void Append<T>(this IList<T> list, IList<T> appendList, bool isRepeat = true)
        {
            foreach (var item in appendList)
                if (isRepeat || !list.Contains(item))
                    list.Add(item);
        }

        /// <summary>
        /// 添加项，仅在IList中没有此项时
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">list</param>
        /// <param name="t">需要添加的值</param>
        public static void AppendNotRepeat<T>(this IList<T> list, T t)
        {
            if (!list.Contains(t))
                list.Add(t);
        }

        /// <summary>
        ///  复制List集合,仅复制引用地址
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IList<T> Clone<T>(this IList<T> list)
        {
            IList<T> newList = new List<T>();
            if (list == null)
                return null;
            foreach (var item in list)
                newList.Add(item);
            return newList;
        }
    }
}
