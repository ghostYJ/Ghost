using System.Collections;
using System.Collections.Generic;

using Ghost.CRUD.Domain;
using Ghost.CRUD.IDao;

using IBatisNet.DataMapper;
namespace Ghost.CRUD.Dao
{
    /// <summary>
    /// 增删改 Dao基类
    /// </summary>
    /// <typeparam name="T">领域对象类型</typeparam>
    /// <typeparam name="U">领域对象Id类型</typeparam>
    public abstract class DaoBase<T, U> : SelectDaoBase<T, U>, IDaoBase<T, U> where T : DomainBase<U>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sqlMapper"></param>
        protected DaoBase(ISqlMapper sqlMapper) : base(sqlMapper)
        {
        }

        #region Insert Update Delete Truncate

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="t">领域对象</param>
        /// <returns>领域对象Id</returns>
        public virtual U Insert(T t)
        {
            if (t == null)
                return default(U);

            _sqlMapper.Insert(GetStatementIdWithNamespace(SqlMapConstants.InsertStatementId), t);

            return t.Id;
        }

        /// <summary>
        /// 更新，根绝领域对象Id更新
        /// </summary>
        /// <param name="t">领域对象</param>
        /// <returns>更新的记录数</returns>
        public virtual int Update(T t)
        {
            if (t == null)
                return default(int);
            return _sqlMapper.Update(GetStatementIdWithNamespace(SqlMapConstants.UpdateStatementId), t);
        }

        /// <summary>
        /// 更新，根据指定领域对象Id更新
        /// </summary>
        /// <param name="id">领域对象Id</param>
        /// <param name="t">领域对象</param>
        /// <returns>更新数量</returns>
        public virtual int Update(U id, T t)
        {
            t.Id = id;
            return Update(t);
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="condition">删除条件，如为null或空则不进行删除</param>
        /// <returns>删除数量</returns>
        protected virtual int Delete(Hashtable condition)
        {
            Hashtable ht = DaoHelper.ProcessConditionHashtable(condition);
            if (ht == null || ht.Count == 0)
                return default(int);
            return _sqlMapper.Delete(GetStatementIdWithNamespace(SqlMapConstants.DeleteStatementId), ht);
        }

        /// <summary>
        /// 根据查询条件类删除
        /// </summary>
        /// <param name="condition">删除条件，如为null或空则不进行删除</param>
        /// <returns>删除数量</returns>
        public virtual int Delete(ICondition condition)
        {
            return Delete(CreateConditionHashtable(condition));
        }

        /// <summary>
        /// 删除所有数据，数据量大时建议调用Truncate方法
        /// </summary>
        /// <returns></returns>
        public virtual int DeleteAll()
        {
            return _sqlMapper.Delete(GetStatementIdWithNamespace(SqlMapConstants.DeleteStatementId), null);
        }

        /// <summary>
        /// 根据Id删除
        /// </summary>
        /// <param name="id">领域对象Id</param>
        /// <returns>删除数量</returns>
        public virtual int DeleteById(U id)
        {
            return Delete(new Hashtable { { SqlMapConstants.IdParam, id } });
        }

        /// <summary>
        /// 根据Id集合删除
        /// </summary>
        /// <param name="ids">领域对象Id集合，如集合为null或为空则不进行删除</param>
        /// <returns></returns>
        public virtual int DeleteByIdList(IList<U> ids)
        {
            if (ids.Count == 0 || ids == null)
                return default(int);
            return Delete(new Hashtable { { SqlMapConstants.IdsParam, ids } });
        }

        /// <summary>
        /// 清空表
        /// </summary>
        public virtual void Truncate()
        {
            _sqlMapper.QueryForObject(GetStatementIdWithNamespace(SqlMapConstants.TruncateStatementId), null);
        }

        #endregion
    }
}
