namespace Ghost.Utility
{
    /// <summary>
    ///  NullableMathToString(可空数据类型转String)
    /// </summary>
    public static class NullableMathExtensionMethod
    {
        /// <summary>
        /// 可空decimal型格式化输出(若为空返回Empty)
        /// </summary>
        /// <param name="d"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToString(this decimal? d, string format)
        {
            if (d.HasValue)
                return d.Value.ToString(format);
            return string.Empty;
        }

        /// <summary>
        /// 可空double型格式化输出(若为空返回Empty)
        /// </summary>
        /// <param name="d"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToString(this double? d, string format)
        {
            if (d.HasValue)
                return d.Value.ToString(format);
            return string.Empty;
        }

        /// <summary>
        /// 可空float型格式化输出(若为空返回Empty)
        /// </summary>
        /// <param name="f"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToString(this float? f, string format)
        {
            if (f.HasValue)
                return f.Value.ToString(format);
            return string.Empty;
        }

        /// <summary>
        /// 可空int型格式化输出(若为空返回Empty)
        /// </summary>
        /// <param name="i"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToString(this int? i, string format)
        {
            if (i.HasValue)
                return i.Value.ToString(format);
            return string.Empty;
        }

        /// <summary>
        /// 可空long型格式化输出(若为空返回Empty)
        /// </summary>
        /// <param name="l"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToString(this long? l, string format)
        {
            if (l.HasValue)
                return l.Value.ToString(format);
            return string.Empty;
        }

        /// <summary>
        /// 将decimal舍入到指定的精度
        /// </summary>
        /// <param name="d"></param>
        /// <param name="decimals">小数位数</param>
        /// <returns></returns>
        public static decimal? Round(this decimal? d, int decimals)
        {
            if (d.HasValue)
                return decimal.Round(d.Value, decimals);
            return null;
        }
    }
}
