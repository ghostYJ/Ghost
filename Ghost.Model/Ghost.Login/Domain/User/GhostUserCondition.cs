using System;

using Ghost.CRUD.Domain;
namespace Ghost.Login.Domain
{
    /// <summary>
    /// 用户查询条件类
    /// </summary>
    [Serializable]
    public class GhostUserCondition : ICondition
    {
        #region RealName
        /// <summary>
        /// 是否根据真实姓名查询
        /// </summary>
        public bool ByRealName { get; set; }

        private string _realName;

        /// <summary>
        /// 要查询的真实姓名
        /// </summary>
        public string RealName
        {
            get { return _realName; }
            set { _realName = value; ByRealName = true; }
        }
        #endregion

        #region NickName
        /// <summary>
        /// 是否根据昵称查询
        /// </summary>
        public bool ByNickName { get; set; }

        private string _nickName;

        /// <summary>
        /// 要查询的昵称
        /// </summary>
        public string NickName
        {
            get { return _nickName; }
            set { _nickName = value; ByNickName = true; }
        }
        #endregion

        #region Mobile
        /// <summary>
        /// 是否根据手机号查询
        /// </summary>
        public bool ByMobile { get; set; }

        private string _mobile;

        /// <summary>
        /// 要查询的手机号
        /// </summary>
        public string Mobile
        {
            get { return _mobile; }
            set { _mobile = value; ByMobile = true; }
        }
        #endregion


        #region Sex
        /// <summary>
        /// 是否根据性别查询
        /// </summary>
        public bool BySex { get; set; }

        private Sex _sex;

        /// <summary>
        /// 要查询的性别
        /// </summary>
        public Sex Sex
        {
            get { return _sex; }
            set { _sex = value; BySex = true; }
        }
        #endregion

        #region VipLevel
        /// <summary>
        /// 是否根据Vip等级查询
        /// </summary>
        public bool ByVipLevel { get; set; }

        private VipLevel _vipLevel;

        /// <summary>
        /// 要查询的Vip等级
        /// </summary>
        public VipLevel VipLevel
        {
            get { return _vipLevel; }
            set { _vipLevel = value; ByVipLevel = true; }
        }
        #endregion

        /// <summary>
        /// 要查询的是否为Vip用户
        /// </summary>
        public bool? IsVip { get; set; }
    }
}
