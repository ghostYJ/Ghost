using System.Collections;

using Ghost.CRUD.Dao;
using Ghost.CRUD.Domain;
using Ghost.Login.Domain;
using Ghost.Login.IDao;

using IBatisNet.DataMapper;

namespace Ghost.Login.Dao
{
    public class GhostUserDao : DaoBase<GhostUser, long>, IGhostUserDao
    {
        public GhostUserDao(ISqlMapper sqlMapper) : base(sqlMapper)
        {
        }

        public override Hashtable CreateConditionHashtable(ICondition condition)
        {
            Hashtable ht = new Hashtable();
            if (condition == null && !(condition is GhostUserCondition))
                return ht;


            GhostUserCondition cond = (GhostUserCondition)condition;
            if (cond.ByRealName)
                ht.Add("RealName", cond.RealName);

            if (cond.ByNickName)
                ht.Add("NickName", cond.NickName);

            if (cond.ByMobile)
                ht.Add("Mobile", cond.Mobile);

            if (cond.IsVip.HasValue)
                ht.Add("IsVip", cond.IsVip.Value);

            if (cond.BySex)
                ht.Add("Sex", cond.Sex);

            if (cond.ByVipLevel)
                ht.Add("VipLevel", cond.VipLevel);

            return ht;
        }

    }
}
