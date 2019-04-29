using System;

using Ghost.CRUD.Domain;
using Ghost.Utility;
namespace Ghost.Login.Domain
{
    /// <summary>
    /// 登录序列
    /// </summary>
    public class LoginSequence : DomainBase<long>
    {
        public LoginSequence()
        {
            Id = SnowFlake.GetNewId();
        }

        /// <summary>
        /// 登录令牌Id
        /// </summary>
        public string LoginTokenId { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime LoginTime { get; set; }

        /// <summary>
        /// 登录IP
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 服务器变量
        /// </summary>
        public string ServerVariables { get; set; }
    }
}
