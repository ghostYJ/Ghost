using System.Collections;
using System.Collections.Generic;

namespace Ghost.Utility
{
    /// <summary>
    /// 泛型字典扩展方法类
    /// </summary>
    public static class GenericDictionaryExtensionMethod
    {
        /// <summary>
        /// 从字典中获取Key值对应的Value，如果没有则返回默认值
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key">Key值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static V GetValue<K, V>(this IDictionary<K, V> dict, K key, V defaultValue)
        {
            if (dict != null && dict.ContainsKey(key))
                return dict[key];
            return defaultValue;
        }

        /// <summary>
        /// 从字典中获取Key值对应的Value，如果没有则返回空
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key">Key值</param>
        /// <returns></returns>
        public static V? GetValueOrNullable<K, V>(this IDictionary<K, V> dict, K key) where V : struct
        {
            if (dict != null && dict.ContainsKey(key))
                return dict[key];
            return null;
        }

        /// <summary>
        /// 从字典中获取Key值对应的Value，如果没有则返回空
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static V GetValueOrNull<K, V>(this IDictionary<K, V> dict, K key) where V : class
        {
            if (dict != null && dict.ContainsKey(key))
                return dict[key];
            return null;
        }

        /// <summary>
        /// 若字典中不包含Key值，则添加相应的Key，Value
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key">Key值</param>
        /// <param name="value">Value值</param>
        public static void AddWhenNotExisted<K, V>(this IDictionary<K, V> dict, K key, V value)
        {
            if (!dict.ContainsKey(key))
                dict.Add(key, value);
        }

        /// <summary>
        ///  若字典中包含Key值则更新Value值，否则添加Key，Value
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key">Key值</param>
        /// <param name="value">Value值</param>
        public static void AddOrUpdate<K, V>(this IDictionary<K, V> dict, K key, V value)
        {
            if (!dict.ContainsKey(key))
                dict.Add(key, value);
            else
                dict[key] = value;
        }

        /// <summary>
        /// Clone字典
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static IDictionary<K, V> Clone<K, V>(this IDictionary<K, V> dict)
        {
            IDictionary<K, V> newDict = new Dictionary<K, V>();
            if (dict != null)
            {
                foreach (KeyValuePair<K, V> item in dict)//IDictionary值类型是KeyValuePair<TKey, TValue>类型
                    newDict.Add(item.Key, item.Value);
            }
            return newDict;
        }
    }

    /// <summary>
    /// 字典扩展方法类
    /// </summary>
    public static class DictionaryExtensionMethod
    {
        /// <summary>
        /// 获取字典Key值对应的Value值，若不存在Key值则返回默认值
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="key">Key值</param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static object GetValue(this IDictionary dict, object key, object defaultValue)
        {
            if (dict != null && dict.Contains(key))
                return dict[key];
            return defaultValue;
        }

        /// <summary>
        /// 获取字典Key值对应的Value值，若不存在Key值则返回空
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="key">Key值</param>
        /// <returns></returns>
        public static object GetValueOrNull(this IDictionary dict, object key)
        {
            if (dict != null && dict.Contains(key))
                return dict[key];
            return null;
        }

        /// <summary>
        /// 当字典中不存在Key值时，添加相应的Key，Value
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="key">Key值</param>
        /// <param name="value">Value值</param>
        public static void AddWhenNotExisted(this IDictionary dict, object key, object value)
        {
            if (!dict.Contains(key))
                dict.Add(key, value);
        }

        /// <summary>
        /// 当字典中不存在Key值时，添加相应的Key，Value；若存在则更新该Key值对应的Value
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="key">Key值</param>
        /// <param name="value">Value值</param>
        public static void AddOrUpdate(this IDictionary dict, object key, object value)
        {
            if (!dict.Contains(key))
                dict.Add(key, value);
            else
                dict[key] = value;
        }
    }
}
