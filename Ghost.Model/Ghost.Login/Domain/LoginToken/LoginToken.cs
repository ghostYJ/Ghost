using System;

using Ghost.CRUD.Domain;
using Ghost.Utility;

namespace Ghost.Login.Domain
{
    /// <summary>
    /// 登录令牌
    /// </summary>
    public class LoginToken : DomainBase<long>
    {
        public LoginToken()
        {
            Id = SnowFlake.GetNewId();
        }

        /// <summary>
        /// 登录用户类型
        /// </summary>
        public string LoginType { get; set; }

        /// <summary>
        /// 登录用户Id
        /// </summary>
        public string LoginId { get; set; }

        /// <summary>
        /// 令牌创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 令牌过期时间
        /// </summary>
        public DateTime? ExpireTime { get; set; }

        /// <summary>
        /// 是否失效
        /// </summary>
        public bool Invalid { get; set; }
    }
}
