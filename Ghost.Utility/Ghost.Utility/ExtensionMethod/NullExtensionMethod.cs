using System;

namespace Ghost.Utility
{
    /// <summary>
    /// 唯为空转换扩展方法
    /// </summary>
    public static class NullExtensionMethod
    {
        /// <summary>
        /// 返回委托类型转换，若调用对象为null返回default()
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="t"></param>
        /// <param name="fn"></param>
        /// <returns></returns>
        public static U IfNotNull<T, U>(this T t, Func<T, U> fn)
        {
            return t != null ? fn(t) : default(U);
        }

        /// <summary>
        /// 返回委托类型转换，若调用对象为null返回自定义参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="t"></param>
        /// <param name="fn"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static U IfNotNull<T, U>(this T t, Func<T, U> fn, U defaultValue)
        {
            return t != null ? fn(t) : defaultValue;
        }
    }
}
