using System.Collections;


namespace Ghost.Utility
{
    /// <summary>
    /// Hashtable扩展方法类
    /// </summary>
    public static class HashtableExtensionMethod
    {
        /// <summary>
        /// 若Hashtable中Key值不存在，则添加Key,Value
        /// </summary>
        /// <param name="ht"></param>
        /// <param name="key">Key值</param>
        /// <param name="value">Value值</param>
        public static void AddWhenNotExisted(this Hashtable ht, object key, object value)
        {
            if (!ht.ContainsKey(key))
                ht.Add(key, value);
        }

        /// <summary>
        /// 若Hashtable中存在Key值，则更新Value值；否则添加Key，Value
        /// </summary>
        /// <param name="ht"></param>
        /// <param name="key">Key值</param>
        /// <param name="value">Value值</param>
        public static void AddOrUpdate(this Hashtable ht, object key, object value)
        {
            if (!ht.ContainsKey(key))
                ht.Add(key, value);
            else;
            ht[key] = value;
        }
    }
}
