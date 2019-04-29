using System;


namespace Ghost.Utility
{
    /// <summary>
    /// 访问权限异常
    /// </summary>
    public class AccessForbiddenException : Exception
    {
        /// <summary>
        /// 构造函数(无参)
        /// </summary>
        public AccessForbiddenException() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        public AccessForbiddenException(string message) : base(message)
        { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public AccessForbiddenException(string message, System.Exception innerException) : base(message, innerException) { }
    }
}
