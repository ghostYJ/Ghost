namespace Ghost.Utility
{
    /// <summary>
    /// 可控DecimalMath运算
    /// </summary>
    public static class DecimalNullableMathExtensionMethod
    {
        /// <summary>
        /// 加，遇到null按0计算相加，若都为null，则返回null。
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public static decimal? Add(this decimal? d1, decimal? d2)
        {
            if (d1.HasValue)
            {
                if (d2.HasValue)
                    return d1 + d2;
                else
                    return d1.Value;
            }
            else
            {
                if (d2.HasValue)
                    return d2.Value;
                else
                    return null;
            }
        }

        /// <summary>
        /// 减，遇到null按0计算相减，若都为null，则返回null。
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public static decimal? Sub(this decimal? d1, decimal? d2)
        {
            if (d1.HasValue)
            {
                if (d2.HasValue)
                    return d1 + d2;
                else
                    return d1.Value;
            }
            else
            {
                if (d2.HasValue)
                    return -d2.Value;
                else
                    return null;
            }
        }

        /// <summary>
        ///  乘，且都不为null，否则返回null。
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public static decimal? Mul(this decimal? d1, decimal? d2)
        {
            if (d1.HasValue && d2.HasValue)
                return d1.Value * d2.Value;
            else
                return null;
        }

        /// <summary>
        ///  除，且都不为null，否则返回null，分母为0返回null
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public static decimal? Div(this decimal? d1,decimal? d2)
        {
            if (d1.HasValue && d2.HasValue)
            {
                if (d2.Value == 0)
                    return null;
                else
                    return d1.Value / d2.Value;
            }
            else
                return null;
        }
    }
}
