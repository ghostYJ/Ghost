using System;
using System.Web.Services;

using Ghost.DDD;
using Ghost.Login.Domain;
using Ghost.Login.IDao;
using Ghost.Utility;

public partial class pages_VerifyLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Login("18502502070","123","true");
    }
    [WebMethod]
    public static string Login(string mobile, string password,string isrember)
    {
        string result;
        GhostUser user = IocContainer.Get<IGhostUserDao>().SelectTop1(new GhostUserCondition { Mobile=mobile.Trim()});
        if (user == null)
            return result = "不存在此手机号";
        if (user.PassWord != password.Trim())
            return result = "密码错误";
        if (user.IsDisabled)
            return result = "用户已被禁用";

        //创建登录令牌
        Auth.Login(user, isrember.ToBoolReq());
        return "True";
    }
}