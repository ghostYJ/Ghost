using System;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using System.Xml.Linq;

using Ghost.Login.Domain;
using Ghost.DDD;
using Ghost.Login.IDao;
using Ghost.Utility;

/// <summary>
//  登录帮助类
/// </summary>
public partial class Auth
{
    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="loginType">登录人类型</param>
    /// <param name="loginId">登录人Id</param>
    /// <param name="isRember">是否记住令牌</param>
    /// <param name="expireTime">失效时间</param>
    public static void Login(string loginType, string loginId, bool isRember, DateTime? expireTime = null)
    {
        DateTime expTime = DateTime.Now;
        if (isRember)
        {
            if (!expireTime.HasValue)
                expTime = DateTime.Now.AddMonths(1);
            else
                expTime = expireTime.Value;
        }
        //创建登录令牌
        LoginToken loginToken = CreateLoginToken(loginType, loginId, expTime);
        //保存令牌到Session
        HttpContext.Current.Session[WebLoginConstants.LoginTokenSession] = loginToken;
        //保存令牌到Cookies,并且设置Cookies不过期
        HttpContext.Current.Response.Cookies[WebLoginConstants.LoginTokenCookies].Value = loginToken.Id.ToString();
        HttpContext.Current.Response.Cookies[WebLoginConstants.LoginTokenCookies].Expires = DateTime.MaxValue;

    }

    /// <summary>
    /// 注销
    /// </summary>
    public static void LoginOut()
    {
        //设置Session为Cancels
        HttpContext.Current.Session.Abandon();

        //设置登录令牌无效
        if (HttpContext.Current.Response.Cookies[WebLoginConstants.LoginTokenCookies] != null)
        {
            string tokenId = HttpContext.Current.Response.Cookies[WebLoginConstants.LoginTokenCookies].Value;
            LoginToken token = IocContainer.Get<ILoginTokenDao>().SelectById(tokenId.ToLongOrDefault());
            if (token != null)
            {
                token.Invalid = true;
                IocContainer.Get<ILoginTokenDao>().Update(token);
            }
        }
    }

    /// <summary>
    /// 恢复登录令牌
    /// 1、先从Session中恢复（LoginToken对象）
    /// 2、若Session中没有，则从Cookies中恢复（LoginToken的Id）
    /// </summary>
    /// <param name="loginToken"></param>
    /// <returns></returns>
    public static bool TryGetLoginToken(out LoginToken loginToken)
    {
        loginToken = null;
        //先从Session中获取令牌
        if (HttpContext.Current.Session[WebLoginConstants.LoginTokenSession] != null)
        {
            loginToken = (LoginToken)HttpContext.Current.Session[WebLoginConstants.LoginTokenSession];
            return true;
        }
        //若Session中不存在，则通过Cookies获取
        if (HttpContext.Current.Response.Cookies[WebLoginConstants.LoginTokenCookies] != null)
        {
            string tokenId = HttpContext.Current.Response.Cookies[WebLoginConstants.LoginTokenCookies].Value;
            LoginToken token = IocContainer.Get<ILoginTokenDao>().SelectById(tokenId.ToLongOrDefault());
            if (token == null)
                return false;
            if (token.Invalid)
                return false;
            if (token.ExpireTime.HasValue && token.ExpireTime < DateTime.Now)
                return false;

            //当Session失效时，重新记录一下登录序号
            RecordLoginSequence(loginToken.Id.ToString());

            //再次存入Seesion
            HttpContext.Current.Session[WebLoginConstants.LoginTokenSession] = token;
            return true;
        }
        return false;
    }

    /// <summary>
    /// 取当前登录用户的有效登录令牌(未过期以及未失效),如无则返回null
    /// </summary>
    /// <returns></returns>
    public static LoginToken GetLoginToken()
    {
        LoginToken token;
        if (TryGetLoginToken(out token))
            return token;
        return null;
    }

    /// <summary>
    /// 取登录令牌
    /// 可以为空
    /// </summary>
    /// <param name="tokenId">登录令牌Id</param>
    /// <returns></returns>
    public static LoginToken GetLoginToken(string tokenId)
    {
        if (string.IsNullOrEmpty(tokenId))
            return null;
        return IocContainer.Get<ILoginTokenDao>().SelectById(tokenId.ToLongOrDefault());
    }

    /// <summary>
    /// 取登录令牌，如为空则抛出异常
    /// </summary>
    /// <param name="tokenId">登录令牌Id</param>
    /// <returns></returns>
    public static LoginToken GetLoginTokenReq(string tokenId)
    {
        LoginToken token = GetLoginToken(tokenId);
        if (token == null)
            throw new LoginTokenNotFoundException("未查询到登录令牌");
        return token;
    }

    /// <summary>
    /// 创建登录令牌
    /// </summary>
    /// <param name="loginType">登录类型</param>
    /// <param name="loginId">登录人Id</param>
    /// <param name="expireTime">失效时间</param>
    /// <returns></returns>
    public static LoginToken CreateLoginToken(string loginType, string loginId, DateTime? expireTime)
    {
        LoginToken loginToken = new LoginToken();
        loginToken.LoginType = loginType;
        loginToken.LoginId = loginId;
        loginToken.CreateTime = DateTime.Now;
        loginToken.ExpireTime = expireTime;
        loginToken.Invalid = false;
        IocContainer.Get<ILoginTokenDao>().Insert(loginToken);
        //新创建LoginToken时记录LoginSequence
        RecordLoginSequence(loginToken.Id.ToString());
        return loginToken;
    }

    /// <summary>
    /// 记录登录序列
    /// </summary>
    /// <param name="loginTokenId">令牌Id</param>
    public static void RecordLoginSequence(string loginTokenId)
    {
        string ip = string.Empty;
        string serverVariables = string.Empty;
        if (HttpContext.Current != null)
        {
            NameValueCollection coll = HttpContext.Current.Request.ServerVariables;
            if (coll.AllKeys.Contains("REMOTE_HOST"))
                ip = coll["REMOTE_HOST"];
            XElement x = new XElement("ServerVariables");
            foreach (string name in coll)
                x.Add(new XElement(name, HttpContext.Current.Request.ServerVariables[name]));
            serverVariables = x.ToString();
        }

        LoginSequence loginSequence = new LoginSequence();
        loginSequence.LoginTokenId = loginTokenId;
        loginSequence.LoginTime = DateTime.Now;
        loginSequence.IP = ip;
        loginSequence.ServerVariables = serverVariables;
        IocContainer.Get<ILoginSequenceDao>().Insert(loginSequence);
    }
}