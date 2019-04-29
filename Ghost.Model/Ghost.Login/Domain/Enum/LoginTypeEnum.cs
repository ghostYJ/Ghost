using Ghost.Utility;

namespace Ghost.Login.Domain
{
    public enum LoginType
    {
        [EnumText("普通用户")]
        User = 1,

        [EnumText("Vip用户")]
        VipUser = 2
    }
}
