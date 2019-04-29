using System.Collections;

using Ghost.CRUD.Dao;
using Ghost.CRUD.Domain;
using Ghost.Login.Domain;
using Ghost.Login.IDao;

using IBatisNet.DataMapper;

namespace Ghost.Login.Dao
{
    public class LoginTokenDao : DaoBase<LoginToken, long>, ILoginTokenDao
    {
        public LoginTokenDao(ISqlMapper sqlMapper) : base(sqlMapper)
        {

        }

        public override Hashtable CreateConditionHashtable(ICondition condition)
        {
            Hashtable ht = new Hashtable();
            if (condition == null || !(condition is LoginTokenCondition))
                return ht;

            LoginTokenCondition con = (LoginTokenCondition)condition;

            return ht;
        }
    }
}
