using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Ghost.CRUD.Domain;
using Ghost.CRUD.IDao;

using IBatisNet.DataMapper;

namespace Ghost.CRUD.Dao
{
    public abstract class RelatedDaoBase<T, U> : RelatedSelectDaoBase<T, U>, IRelatedDaoBase<T, U> where T : RelatedDomainBase<U>
    {
        /// <summary>
        /// 构造函数，注入ISqlMapper，调用父类的初始化
        /// </summary>
        /// <param name="sqlMapper"></param>
        public RelatedDaoBase(ISqlMapper sqlMapper) : base(sqlMapper)
        {
        }

        #region CreateRelatedConditionHashtable

        protected Hashtable CreateRelatedConditionHashtable(string relatedDomain, string relatedDomainId, Hashtable condition)
        {
            Hashtable ht = new Hashtable();
            foreach (DictionaryEntry entry in condition)
                ht.Add(entry.Key, entry.Value);

            relatedDomain = relatedDomain ?? string.Empty;
            relatedDomainId = relatedDomainId ?? string.Empty;


            if (ht.ContainsKey(SqlMapConstants.UpdateRelatedDomainParam))
                ht[SqlMapConstants.UpdateRelatedDomainParam] = relatedDomain;
            else
                ht.Add(SqlMapConstants.UpdateRelatedDomainParam, relatedDomain);

            if (ht.ContainsKey(SqlMapConstants.UpdateRelatedDomainIdParam))
                ht[SqlMapConstants.UpdateRelatedDomainIdParam] = relatedDomainId;
            else
                ht.Add(SqlMapConstants.UpdateRelatedDomainIdParam, relatedDomainId);

            return ht;
        }

        #endregion

        #region DaoBase方法

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
        /// 更新，根据领域对象Id更新
        /// </summary>
        /// <param name="t">领域对象</param>
        /// <returns>更新记录数</returns>
        public virtual int Update(T t)
        {
            if (t == null)
                return 0;
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
                return 0;
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
        /// <returns>删除数量</returns>
        public virtual int DeleteAll()
        {
            return _sqlMapper.Delete(
                GetStatementIdWithNamespace(SqlMapConstants.DeleteStatementId), null);
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
        /// <param name="idList">领域对象Id集合，如集合为null或空则不进行删除</param>
        /// <returns></returns>
        public virtual int DeleteByIdList(IList<U> idList)
        {
            if (idList == null || idList.Count == 0)
                return 0;

            return Delete(new Hashtable { { SqlMapConstants.IdsParam, idList } });
        }

        /// <summary>
        /// 清空表
        /// </summary>
        public virtual void Truncate()
        {
            _sqlMapper.QueryForObject(GetStatementIdWithNamespace(SqlMapConstants.TruncateStatementId), null);
        }

        #endregion

        #endregion

        #region InsertRelated

        /// <summary>
        /// 插入关联领域对象数据
        /// </summary>
        /// <param name="t">领域对象</param>
        /// <param name="relatedDomain">关联领域对象</param>
        /// <param name="relatedDomainId">关联领域对象Id</param>
        /// <returns>领域对象Id</returns>
        public U Insert(T t,string relatedDomain,string relatedDomainId)
        {
            relatedDomain = relatedDomain ?? string.Empty;
            relatedDomainId = relatedDomainId ?? string.Empty;

            t.RelatedDomain = relatedDomain;
            t.RelatedDomainId = relatedDomainId;

            return Insert(t);
        }
        #endregion

        #region UpdateRelated

        /// <summary>
        /// 根据查询条件更新关联领域对象(虚方法，可重写)
        /// </summary>
        /// <param name="relatedDomain">更新关联领域对象的值</param>
        /// <param name="relatedDomainId">更新关联领域对象Id的值</param>
        /// <param name="condition">查询条件</param>
        /// <returns>更新记录数</returns>
        public virtual int UpdateRelated(string relatedDomain, string relatedDomainId, Hashtable condition)
        {
            if (condition == null || condition.Count == 0)
                return default(int);

            Hashtable ht = CreateRelatedConditionHashtable(relatedDomain, relatedDomainId, condition);

            ht = DaoHelper.ProcessConditionHashtable(ht);

            return _sqlMapper.Update(GetStatementIdWithNamespace(SqlMapConstants.UpdateRelatedStatementId), ht);
        }

        /// <summary>
        /// 根据查询条件更新关联领域对象
        /// </summary>
        /// <param name="relatedDomain">更新关联领域对象的值</param>
        /// <param name="relatedDomainId">更新关联领域对象Id的值</param>
        /// <param name="condition">查询条件</param>
        /// <returns>更新记录数</returns>
        public int UpdateRelated(string relatedDomain, string relatedDomainId, ICondition condition)
        {
            return UpdateRelated(relatedDomain, relatedDomainId, CreateConditionHashtable(condition));
        }

        /// <summary>
        /// 根据领域对象Id更新关联领域对象
        /// </summary>
        /// <param name="relatedDomain">更新关联领域对象的值</param>
        /// <param name="relatedDomainId">更新关联领域对象Id的值</param>
        /// <param name="id">领域对象Id</param>
        /// <returns>更新记录数</returns>
        public int UpdateRelated(string relatedDomain, string relatedDomainId, U id)
        {
            return UpdateRelated(relatedDomain, relatedDomainId, new Hashtable { { SqlMapConstants.IdParam, id } });
        }

        /// <summary>
        /// 根据领域对象更新关联领域对象
        /// </summary>
        /// <param name="relatedDomain">更新关联领域对象的值</param>
        /// <param name="relatedDomainId">更新关联领域对象Id的值</param>
        /// <param name="t">领域对象,如为null则不更新</param>
        /// <returns>更新记录数</returns>
        public int UpdateRelated(string relatedDomain, string relatedDomainId, T t)
        {
            if (t == null)
                return default(int);
            return UpdateRelated(relatedDomain, relatedDomainId, t.Id);
        }

        /// <summary>
        /// 根据领域对象Id集合更新关联领域对象
        /// </summary>
        /// <param name="relatedDomain">更新关联领域对象的值</param>
        /// <param name="relatedDomainId">更新关联领域对象Id的值</param>
        /// <param name="idList">领域对象Id集合，如为null或空则不更新</param>
        /// <returns>更新记录数</returns>
        public int UpdateRelated(string relatedDomain, string relatedDomainId, IList<U> idList)
        {
            if (idList == null || idList.Count == 0)
                return default(int);
            return UpdateRelated(relatedDomain, relatedDomainId, new Hashtable { { SqlMapConstants.RelatedDomainIdsParam, idList } });
        }

        /// <summary>
        /// 根据领域对象集合更新关联领域对象
        /// </summary>
        /// <param name="relatedDomain">更新关联领域对象的值</param>
        /// <param name="relatedDomainId">更新关联领域对象Id的值</param>
        /// <param name="domainList">领域对象集合，如为null或空则不更新</param>
        /// <returns>更新记录数</returns>
        public int UpdateRelated(string relatedDomain, string relatedDomainId, IList<T> domainList)
        {
            if (domainList == null || domainList.Count == 0)
                return default(int);

            return UpdateRelated(relatedDomain, relatedDomainId, domainList.Select(s => s.Id).ToList());
        }
        #endregion

        #region RestoreRelated

        /// <summary>
        /// 根据查询条件重置关联关系为空(虚方法,可重写)
        /// </summary>
        /// <param name="condition">查询条件，如为空或未设置查询条件则不更新</param>
        /// <returns>重置记录数</returns>
        protected virtual int RestoreRelated(Hashtable condition)
        {
            return UpdateRelated(string.Empty, string.Empty, condition);
        }

        /// <summary>
        /// 根据查询条件重置关联关系为空
        /// </summary>
        /// <param name="condition">查询条件，如为空或未设置查询条件则不更新</param>
        /// <returns>重置记录数</returns>
        public int RestoreRelated(ICondition condition)
        {
            return UpdateRelated(string.Empty, string.Empty, condition);
        }

        /// <summary>
        /// 根据领域对象重置关联关系为空
        /// </summary>
        /// <param name="t">领域对象</param>
        /// <returns>重置记录数</returns>
        public int RestoreRelated(T t)
        {
            return UpdateRelated(string.Empty, string.Empty, t.Id);
        }

        /// <summary>
        /// 根据领域对象Id重置关联关系为空
        /// </summary>
        /// <param name="id">领域对象Id</param>
        /// <returns>重置记录数</returns>
        public int RestoreRelated(U id)
        {
            return UpdateRelated(string.Empty, string.Empty, id);
        }

        /// <summary>
        /// 根据领域对象集合重置关联关系为空
        /// </summary>
        /// <param name="domainList">领域对象集合</param>
        /// <returns>重置记录数</returns>
        public int RestoreRelated(IList<T> domainList)
        {
            return UpdateRelated(string.Empty, string.Empty, domainList);
        }

        /// <summary>
        /// 根据领域对象及领域对象Id重置关联关系为空
        /// </summary>
        /// <param name="relatedDomain">领域对象</param>
        /// <param name="relatedDomainId">领域对象Id</param>
        /// <returns></returns>
        public int RestoreRelated(IList<U> domainIdList)
        {
            return UpdateRelated(string.Empty, string.Empty, domainIdList);
        }

        /// <summary>
        /// 根据领域对象及领域对象Id重置关联关系为空
        /// </summary>
        /// <param name="relatedDomain">领域对象</param>
        /// <param name="relatedDomainId">领域对象Id</param>
        /// <returns></returns>
        public int RestoreRelated(string relatedDomain, string relatedDomainId)
        {
            //加载RelatedSelectDaoBase内CreateRelatedConditionHashtable方法构造查询Hashtable
            return RestoreRelated(CreateRelatedConditionHashtable(null, relatedDomain, true, relatedDomainId));
        }

        #endregion

        #region DeleteByRelated

        /// <summary>
        /// 根据关联领域对象删除关联数据
        /// </summary>
        /// <param name="relatedDomain">关联领域对象</param>
        /// <param name="condition">查询条件</param>
        /// <returns>删除记录数</returns>
        public int DeleteByRelated(string relatedDomain, ICondition condition = null)
        {
            return Delete(CreateRelatedConditionHashtable(condition, relatedDomain));
        }

        /// <summary>
        /// 根据关联领域对象及关联领域对象Id删除关联数据
        /// </summary>
        /// <param name="relatedDomain">关联领域对象</param>
        /// <param name="relatedDomainId">关联领域对象Id</param>
        /// <param name="condition">查询条件</param>
        /// <returns>删除记录数</returns>
        public int DeleteByRelated(string relatedDomain, string relatedDomainId, ICondition condition = null)
        {
            return Delete(CreateRelatedConditionHashtable(condition, relatedDomain, true, relatedDomainId));
        }

        public int DeleteByRelated(string relatedDomain, IList<string> relatedDomainIdList, ICondition condition = null)
        {
            return Delete(CreateRelatedConditionHashtable(condition, relatedDomain, false, string.Empty,relatedDomainIdList));
        }
        #endregion
    }
}
