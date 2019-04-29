using System;

namespace Ghost.Utility
{
    /// <summary>
    /// 日期处理扩展方法
    /// </summary>
    public static class DateTimeExtensionMethod
    {
        /// <summary>
        /// 明天
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime Tomorrow(this DateTime date)
        {
            return date.AddDays(1);
        }

        /// <summary>
        /// 昨天
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime Yesterday(this DateTime date)
        {
            return date.AddDays(-1);
        }

        /// <summary>
        /// 给定月份的第一天
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetFirstDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        /// <summary>
        /// 给定月份的最后一天
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetLastDayOfMonth(this DateTime date)
        {
            return date.GetFirstDayOfMonth().AddMonths(1).AddDays(-1);
        }

        /// <summary>
        /// 是否不等于给定的日期
        /// </summary>
        /// <param name="date"></param>
        /// <param name="compareDateTime"></param>
        /// <returns></returns>
        public static bool IsEQU(this DateTime date, DateTime compareDateTime)
        {
            return date.CompareTo(compareDateTime) == 0;
        }

        /// <summary>
        /// 是否小于给定的日期
        /// </summary>
        /// <param name="date"></param>
        /// <param name="compareDateTime"></param>
        /// <returns></returns>
        public static bool IsLSS(this DateTime date, DateTime compareDateTime)
        {
            return date.CompareTo(compareDateTime) < 0;
        }

        /// <summary>
        /// 是否小于等于给定的日期
        /// </summary>
        /// <param name="date"></param>
        /// <param name="compareDateTime"></param>
        /// <returns></returns>
        public static bool IsLEQ(this DateTime date, DateTime compareDateTime)
        {
            return date.CompareTo(compareDateTime) <= 0;
        }

        /// <summary>
        /// 是否大于给定的日期
        /// </summary>
        /// <param name="date"></param>
        /// <param name="compareDateTime"></param>
        /// <returns></returns>
        public static bool IsGTR(this DateTime date, DateTime compareDateTime)
        {
            return date.CompareTo(compareDateTime) > 0;
        }

        /// <summary>
        /// 是否大于等于给定的日期
        /// </summary>
        /// <param name="date"></param>
        /// <param name="compareDateTime"></param>
        /// <returns></returns>
        public static bool IsGEQ(this DateTime date, DateTime compareDateTime)
        {
            return date.CompareTo(compareDateTime) >= 0;
        }

        /// <summary>
        /// 给定日期最后一刻，精确到 23:59:59.999
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime EndTimeOfDay(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
        }

        /// <summary>
        /// 给定日期开始一刻，精确到00:00:00.000
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime StartTimeOfDay(this DateTime date)
        {
            return date.Date;
        }

        /// <summary>
        /// 判断是否是今天
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool IsToday(this DateTime date)
        {
            return (date.Date == DateTime.Now.Date);
        }

        /// <summary>
        /// 可空DateTime型格式化输出(若为空返回Empty)
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToString(this DateTime? dt, string format)
        {
            if (dt.HasValue)
                return dt.Value.ToString(format);
            return string.Empty;
        }
    }
}
