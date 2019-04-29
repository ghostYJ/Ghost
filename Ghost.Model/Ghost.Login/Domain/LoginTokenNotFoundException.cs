using System;

namespace Ghost.Login.Domain
{
    /// <summary>
    /// 登录令牌不存在异常类
    /// </summary>
    public class LoginTokenNotFoundException : Exception
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public LoginTokenNotFoundException() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        public LoginTokenNotFoundException(string message) : base(message) { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public LoginTokenNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
