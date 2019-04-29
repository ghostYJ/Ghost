using System;

namespace Ghost.Utility
{
    /// <summary>
    /// 唯一性异常
    /// </summary>
    public class UniqueException : Exception
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public UniqueException() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        public UniqueException(string message) : base(message) { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public UniqueException(string message, System.Exception innerException) : base(message, innerException) { }
    }
}
