namespace Ghost.Login.Domain
{
    /// <summary>
    /// 定义在Web应用中用到的键名，包括Session、Cookies
    /// Web应用中不能与此处定义的键名重复
    public class WebLoginConstants
    {
        /// <summary>
        /// 登录令牌保存在Session中的键名，值为“Ghost_Model_LoginTokenSession”
        /// </summary>
        public const string LoginTokenSession = "Ghost_Model_LoginTokenSession";

        /// <summary>
        /// 登录令牌保存在Cookies中的键名，值为“Ghost_Model_LoginTokenCookies”
        /// </summary>
        public const string LoginTokenCookies = "Ghost_Model_LoginTokenCookies";
    }
}
