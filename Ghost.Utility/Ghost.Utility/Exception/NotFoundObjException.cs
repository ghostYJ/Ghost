using System;

namespace Ghost.Utility
{
    /// <summary>
    /// 对象不存在异常
    /// </summary>
    public class NotFoundObjException : Exception
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public NotFoundObjException() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        public NotFoundObjException(string message) : base(message) { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public NotFoundObjException(string message, System.Exception innerException) : base(message, innerException) { }
    }
}
