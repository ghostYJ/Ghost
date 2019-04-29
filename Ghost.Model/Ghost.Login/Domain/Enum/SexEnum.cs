using Ghost.Utility;

namespace Ghost.Login.Domain
{
    /// <summary>
    /// 用户性别枚举
    /// </summary>
    public enum Sex
    {
        [EnumText("男")]
        男 = 1,

        [EnumText("女")]
        女 = 2,

        [EnumText("保密")]
        保密 = 3
    }
}
