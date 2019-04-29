using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

using Ghost.CRUD.Domain;
using Ghost.CRUD.IDao;

using IBatisNet.DataMapper;
namespace Ghost.CRUD.Dao
{
    /// <summary>
    /// 关联领域对象查询Dao基类
    /// </summary>
    /// <typeparam name="T">领域对象类型</typeparam>
    /// <typeparam name="U">领域对象Id类型</typeparam>
    public abstract class RelatedSelectDaoBase<T, U> : SelectDaoBase<T, U>, IRelatedSelectDaoBase<T, U> where T : RelatedDomainBase<U>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sqlMapper"></param>
        protected RelatedSelectDaoBase(ISqlMapper sqlMapper) : base(sqlMapper)
        {
        }

        /// <summary>
        /// 生成查询Hashtable
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="relatedDomain">关联领域对象</param>
        /// <param name="relatedDomainIdFlag">是否加载关联领域对象Id(默认为False)</param>
        /// <param name="relatedDomainId">关联领域对象Id(可空)</param>
        /// <param name="relatedDomainIdList">关联领域对象Id集合（可空）</param>
        /// <returns>查询Hashtable</returns>
        protected Hashtable CreateRelatedConditionHashtable(ICondition condition, string relatedDomain, bool relatedDomainIdFlag = false, string relatedDomainId = "", IList<string> relatedDomainIdList = null)
        {
            Hashtable ht = CreateConditionHashtable(condition) ?? new Hashtable();

            relatedDomain = relatedDomain ?? string.Empty;
            relatedDomainId = relatedDomainId ?? string.Empty;

            if (ht.ContainsKey(SqlMapConstants.RelatedDomainParam))
                ht[SqlMapConstants.RelatedDomainParam] = relatedDomain;
            else
                ht.Add(SqlMapConstants.RelatedDomainParam, relatedDomain);

            if (relatedDomainIdFlag)
            {
                if (ht.ContainsKey(SqlMapConstants.RelatedDomainIdParam))
                    ht[SqlMapConstants.RelatedDomainIdParam] = relatedDomainId;
                else
                    ht.Add(SqlMapConstants.RelatedDomainIdParam, relatedDomainId);
            }

            if (relatedDomainIdList != null)
            {
                if (ht.ContainsKey(SqlMapConstants.RelatedDomainIdsParam))
                    ht[SqlMapConstants.RelatedDomainIdsParam] = relatedDomainIdList;
                else
                    ht.Add(SqlMapConstants.RelatedDomainIdsParam, relatedDomainIdList);
            }
            return ht;
        }

        #region Select

        /// <summary>
        /// 查询关联领域对象
        /// </summary>
        /// <param name="relatedDomain">关联领域对象</param>
        /// <param name="condition">查询条件</param>
        /// <param name="orderPropertyColl">排序条件</param>
        /// <returns>查询结果</returns>
        public IList<T> SelectRelated(string relatedDomain, ICondition condition, NameValueCollection orderPropertyColl = null)
        {
            return Select(CreateRelatedConditionHashtable(condition, relatedDomain), orderPropertyColl);
        }

        /// <summary>
        /// 查询关联领域对象
        /// </summary>
        /// <param name="relatedDomain">关联领域对象</param>
        /// <param name="relatedDomainId">关联领域对象Id</param>
        /// <param name="condition">查询条件</param>
        /// <param name="orderPropertyColl">排序条件</param>
        /// <returns>查询结果</returns>
        public IList<T> SelectRelated(string relatedDomain, string relatedDomainId, ICondition condition = null, NameValueCollection orderPropertyColl = null)
        {
            return Select(CreateRelatedConditionHashtable(condition, relatedDomain, true, relatedDomainId), orderPropertyColl);
        }

        /// <summary>
        /// 查询关联领域对象
        /// </summary>
        /// <param name="relatedDomain">关联领域对象</param>
        /// <param name="relatedDomainIdList">关联领域对象Id集合,若为null或无子项，则返回无子项集合,new List(）</param>
        /// <param name="condition">查询条件</param>
        /// <param name="orderPropertyColl">排序条件</param>
        /// <returns>查询结果</returns>
        public IList<T> SelectRelated(string relatedDomain, IList<string> relatedDomainIdList, ICondition condition = null, NameValueCollection orderPropertyColl = null)
        {
            if (relatedDomainIdList == null || relatedDomainIdList.Count == 0)
                return new List<T>();

            return Select(CreateRelatedConditionHashtable(condition, relatedDomain, false, string.Empty, relatedDomainIdList), orderPropertyColl);
        }

        #endregion

        #region SelectCount

        /// <summary>
        /// 查询关联领域对象数量
        /// </summary>
        /// <param name="relatedDomain">关联领域对象</param>
        /// <param name="condition">查询条件</param>
        /// <returns>查询数量结果</returns>
        public int SelectRelatedCount(string relatedDomain, ICondition condition = null)
        {
            return SelectCount(CreateRelatedConditionHashtable(condition, relatedDomain));
        }

        /// <summary>
        /// 查询关联领域对象数量
        /// </summary>
        /// <param name="relatedDomain">关联领域对象</param>
        /// <param name="relatedDomainId">关联领域对象Id</param>
        /// <param name="condition">查询条件</param>
        /// <returns>查询数量结果</returns>
        public int SelectRelatedCount(string relatedDomain, string relatedDomainId, ICondition condition = null)
        {
            return SelectCount(CreateRelatedConditionHashtable(condition, relatedDomain, true, relatedDomainId));
        }

        /// <summary>
        /// 查询关联领域对象数量
        /// </summary>
        /// <param name="relatedDomain">关联领域对象</param>
        /// <param name="relatedDomainIdList">关联领域对象Id集合，若集合为null或无子项，则不按此条件查询</param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public int SelectRelatedCount(string relatedDomain, IList<string> relatedDomainIdList, ICondition condition = null)
        {
            return SelectCount(CreateRelatedConditionHashtable(condition, relatedDomain, false, string.Empty, relatedDomainIdList));
        }
        #endregion

        #region SelectByPage

        /// <summary>
        /// 根据关联领域对象分页查询
        /// </summary>
        /// <param name="startRecordIndex">当前页数据记录的起始索引，从1开始</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="relatedDomain">关联领域对象</param>
        /// <param name="condition">查询条件</param>
        /// <param name="orderPropertyColl">排序条件</param>
        /// <returns>查询结果</returns>
        public IList<T> SelectRelatedByPage(int startRecordIndex, int pageSize, string relatedDomain, ICondition condition = null, NameValueCollection orderPorpertyColl = null)
        {
            return SelectByPage(startRecordIndex, pageSize, CreateRelatedConditionHashtable(condition, relatedDomain), orderPorpertyColl);
        }

        /// <summary>
        /// 根据关联领域对象分页查询
        /// </summary>
        /// <param name="startRecordIndex">当前页数据记录的起始索引，从1开始</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="relatedDomain">关联领域对象</param>
        /// <param name="relatedDomainId">关联领域对象Id</param>
        /// <param name="condition">查询条件</param>
        /// <param name="orderPropertyColl">排序条件</param>
        /// <returns>查询结果</returns>
        public IList<T> SelectRelatedByPage(int startReocrdIndex, int pageSize, string relatedDomain, string relatedDomainId, ICondition condition = null, NameValueCollection orderPropertyColl = null)
        {
            return SelectByPage(startReocrdIndex, pageSize, CreateRelatedConditionHashtable(condition, relatedDomain, true, relatedDomainId), orderPropertyColl);
        }

        /// <summary>
        /// 根据关联领域对象分页查询
        /// </summary>
        /// <param name="startRecordIndex">当前页数据记录的起始索引，从1开始</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="relatedDomain">关联领域对象</param>
        /// <param name="relatedDomainIdList">关联领域对象Id集合，若集合为null或无子项,则返回空。</param>
        /// <param name="condition">查询条件</param>
        /// <param name="orderPropertyColl">排序条件</param>
        /// <returns>查询结果</returns>
        public IList<T> SelectRelatedByPage(int startRecordIndex, int pageSize, string relatedDomain, IList<string> relatedDomainIdList, ICondition condition = null, NameValueCollection orderProperty = null)
        {
            return SelectByPage(startRecordIndex, pageSize, CreateRelatedConditionHashtable(condition, relatedDomain, false, string.Empty, relatedDomainIdList));
        }

        #endregion
    }
}
