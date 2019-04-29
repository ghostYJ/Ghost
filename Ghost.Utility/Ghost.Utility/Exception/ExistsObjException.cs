using System;


namespace Ghost.Utility
{
    /// <summary>
    /// 对象已存在异常
    /// </summary>
    public class ExistsObjException : Exception
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ExistsObjException() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        public ExistsObjException(string message) : base(message) { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ExistsObjException(string message, System.Exception innerException) : base(message, innerException) { }
    }
}
