using System.Collections;

using Ghost.CRUD.Dao;
using Ghost.CRUD.Domain;
using Ghost.Login.Domain;
using Ghost.Login.IDao;

using IBatisNet.DataMapper;

namespace Ghost.Login.Dao
{
    public class LoginSequenceDao : DaoBase<LoginSequence, long>, ILoginSequenceDao
    {
        public LoginSequenceDao(ISqlMapper sqlMapper) : base(sqlMapper)
        {
        }

        public override Hashtable CreateConditionHashtable(ICondition condition)
        {
            Hashtable ht = new Hashtable();
            if (condition == null || !(condition is LoginSequenceCondition))
                return ht;

            return ht;
        }
    }
}
