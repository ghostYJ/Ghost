using System;
using System.Linq;

namespace Ghost.Utility
{
    /// <summary>
    /// Enum扩展方法类
    /// </summary>
    public static class EnumExtensionMethod
    {
        /// <summary>
        /// 取T枚举对象的字符型Attribute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetStringAttribute<T>(this Enum e) where T : EnumStringAttribute//约束T必须继承(实现)枚举String特性
        {
            if (e == null)
                return string.Empty;
            //获取对象的枚举类型，在枚举类型中找到该对象具有指定名称的公共字段，获取该公共字段且有T标识的自定义特性的数组
            var attributes = (T[])e.GetType().GetField(e.ToString()).GetCustomAttributes(typeof(T), false);
            if (attributes.Count() > 0)
                return attributes.First().Value;// 只返回第一次定义的EnumStringAttribute特性
            return string.Empty;
        }

        /// <summary>
        /// 获取某个对象的EnumText特性，如果没有设置EnumTextAttribute，则取Name值
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetText(this Enum e)
        {
            if (e == null)
                return string.Empty;
            var attributes = (EnumTextAttribute[])e.GetType().GetField(e.ToString()).GetCustomAttributes(typeof(EnumTextAttribute), false);//这里能取到多个值的原因，可能是inherit属性为true时，找到了继承链里，其他地方重写的EnumText特性的值。
            if (attributes.Count() > 0)
                return attributes.First().Value;
            return e.ToString();
        }

        /// <summary>
        /// 取某个枚举值
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static object GetValue(this Enum e)
        {
            if (e == null)
                return null;
            return e.GetType().GetField(e.ToString()).GetRawConstantValue();//返回与Text相关的Value值
        }

        /// <summary>
        /// 取某个对象的EnumDisabledAttribute属性，如果没有设置EnumDisabledAttribute，返回False。
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>

        public static bool IsDisabled(this Enum e)
        {
            if (e == null)
                return false;
            var attributes = (EnumDisabledAttribute[])e.GetType().GetField(e.ToString()).GetCustomAttributes(typeof(EnumDisabledAttribute), false);
            if (attributes.Count() > 0)
                return
                    attributes.First().Value;
            return false;
        }

        /// <summary>
        /// 取某个对象的EnumIndex属性，如果没有设置EnumIndexAttribute，则取枚举值的Value。
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetIndex(this Enum e)
        {
            if (e == null)
                return string.Empty;
            var attributes = (EnumIndexAttribute[])e.GetType().GetField(e.ToString()).GetCustomAttributes(typeof(EnumIndexAttribute), false);
            if (attributes.Count() > 0)
                return attributes.First().Value;
            return e.GetValue().ToString();
        }
    }
}
