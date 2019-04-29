using Ghost.Utility;

namespace Ghost.Login.Domain
{
    /// <summary>
    /// VIP等级
    /// </summary>
    public enum VipLevel
    {
        [EnumText("VipI")]
        VipI=1,

        [EnumText("VipII")]
        VipII = 2,

        [EnumText("VipIII")]
        VipIII = 3,

        [EnumText("VipIV")]
        VipIV = 4,

        [EnumText("VipV")]
        VipV=5,

        [EnumText("VipVI")]
        VipVI=6,
    }
}
