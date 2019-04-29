using Ghost.DDD;
using Ghost.Login.Domain;
using Ghost.Login.IDao;
using Ghost.Utility;

/// <summary>
/// GhostUser 的摘要说明
/// </summary>
public partial class Auth
{
    /// <summary>
    /// 根据用户类型自动跳转到登录页面
    /// </summary>
    /// <param name="loginType"></param>
    private static void Relogin(string loginType = "")
    {
        //if (loginType == "xxx")
        //    xxxRelogin();
        return;

        //WebMessageHelper.ShowUnBack("未登录或由于闲置时间太长，登录会话已结束，请重新登录。");
    }

    /// <summary>
    /// 获取当前登录类型，未登录时会转到登录页面
    /// </summary>
    public static string LoginType
    {
        get
        {
            LoginToken loginToken;
            if (TryGetLoginToken(out loginToken))
                return loginToken.LoginType;

            Relogin(loginToken.IfNotNull(s => s.LoginType, string.Empty));
            return string.Empty;
        }
    }

    /// <summary>
    /// 获取当前登录Id，未登录时会转到登录页面
    /// </summary>
    public static string LoginId
    {
        get
        {
            LoginToken loginToken;
            if (TryGetLoginToken(out loginToken))
                return loginToken.LoginId;

            Relogin(loginToken.IfNotNull(s => s.LoginType, string.Empty));
            return string.Empty;
        }
    }

    /// <summary>
    /// 获取登录用户，如未登录返回null
    /// </summary>
    public static GhostUser Get()
    {
        LoginToken token = GetLoginToken();
        if (token == null)
            return null;

        long? id = token.LoginId.ToLong();
        if (!id.HasValue)
            return null;

        GhostUser user = IocContainer.Get<IGhostUserDao>().SelectByIdReq(id.Value);

        if (user == null)
            return null;

        if (user.IsDisabled)
            return null;
        return user;
    }

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="user">用户</param>
    /// <param name="isRember">是否记住令牌</param>
    public static void Login(GhostUser user, bool isRember)
    {
        Login(ActorDefine.ACTOR_TYPE_USER, user.Id.ToString(), isRember);
    }

}