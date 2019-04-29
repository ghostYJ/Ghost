using System;

namespace Ghost.Utility
{
    public class ServiceException : Exception
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ServiceException() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        public ServiceException(string message) : base(message) { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ServiceException(string message, Exception innerException) : base(message, innerException) { }
    }
}
