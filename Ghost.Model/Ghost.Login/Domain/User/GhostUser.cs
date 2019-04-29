using System;

using Ghost.CRUD.Domain;
using Ghost.Utility;
namespace Ghost.Login.Domain
{
    /// <summary>
    /// 用户
    /// </summary>
    public class GhostUser : DomainBase<long>
    {
        /// <summary>
        /// 单例
        /// </summary>
        public static GhostUser Default => new GhostUser();
        public GhostUser()
        {
            Id = SnowFlake.GetNewId();
        }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 用户真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Sex? Sex { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 地市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime BirthDay { get; set; }

        /// <summary>
        /// 是否是Vip用户
        /// </summary>
        public bool IsVip { get; set; }

        /// <summary>
        /// VIP等级
        /// </summary>
        public VipLevel VipLevel { get; set; }

        /// <summary>
        /// 账户余额
        /// </summary>
        public decimal AccountBalance { get; set; }

        /// <summary>
        /// 是否被禁用
        /// </summary>
        public bool IsDisabled { get; set; }
    }
}
