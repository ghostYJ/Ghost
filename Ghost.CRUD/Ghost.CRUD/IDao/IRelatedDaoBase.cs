using Ghost.CRUD.Domain;
using System.Collections.Generic;

namespace Ghost.CRUD.IDao
{
    /// <summary>
    /// 关联领域对象数据访问接口
    /// </summary>
    /// <typeparam name="T">领域对象类型</typeparam>
    /// <typeparam name="U">领域对象Id类型</typeparam>
    public interface IRelatedDaoBase<T, U> : IRelatedSelectDaoBase<T, U>, IDaoBase<T, U> where T : RelatedDomainBase<U>
    {
        #region InsertRelated

        /// <summary>
        /// 插入关联领域对象数据
        /// </summary>
        /// <param name="t">领域对象</param>
        /// <param name="relatedDomain">关联领域对象</param>
        /// <param name="relatedDomainId">关联领域对象Id</param>
        /// <returns>领域对象Id</returns>
        U Insert(T t, string relatedDomain, string relatedDomainId);

        #endregion

        #region UpdateRelated

        /// <summary>
        /// 根据查询条件更新关联领域对象
        /// </summary>
        /// <param name="relatedDomain">需要更新的领域对象</param>
        /// <param name="relatedDomainId">需要更新的领域对象Id</param>
        /// <param name="condition">查询条件，如为null则不更新</param>
        /// <returns>更新记录数</returns>
        int UpdateRelated(string relatedDomain, string relatedDomainId, ICondition condition);

        /// <summary>
        /// 根据ID更新关联领域对象
        /// </summary>
        /// <param name="relatedDomain">需要更新的领域对象</param>
        /// <param name="relatedDomainId">需要更新的领域对象Id</param>
        /// <param name="id">领域对象Id</param>
        /// <returns>更新记录数</returns>
        int UpdateRelated(string relatedDomain, string relatedDomainId, U id);

        /// <summary>
        /// 根据领域对象更新关联领域对象
        /// </summary>
        /// <param name="relatedDomain">需要更新的领域对象</param>
        /// <param name="relatedDomainId">需要更新的领域对象Id</param>
        /// <param name="t">领域对象</param>
        /// <returns>更新记录数</returns>
        int UpdateRelated(string relatedDomain, string relatedDomainId,
            T t);

        /// <summary>
        /// 根据Id集合更新关联领域对象
        /// </summary>
        /// <param name="relatedDomain">更新关联领域对象的值</param>
        /// <param name="relatedDomainId">更新关联领域对象Id的值</param>
        /// <param name="idList">Id集合</param>
        /// <returns>更新记录数</returns>
        int UpdateRelated(string relatedDomain, string relatedDomainId, IList<U> idList);

        /// <summary>
        /// 根据领域对象集合更新关联领域对象
        /// </summary>
        /// <param name="relatedDomain">更新关联领域对象的值</param>
        /// <param name="relatedDomainId">更新关联领域对象Id的值</param>
        /// <param name="domainList">领域对象集合</param>
        /// <returns>更新记录数</returns>
        int UpdateRelated(string relatedDomain, string relatedDomainId, IList<T> domainList);
        #endregion

        #region RestoreRelated

        /// <summary>
        /// 根据查询条件重置关联关系为空
        /// </summary>
        /// <param name="condition">查询条件，如为空或未设置查询条件则不更新</param>
        /// <returns>重置记录数</returns>
        int RestoreRelated(ICondition condition);

        /// <summary>
        /// 根据领域对象重置关联关系为空
        /// </summary>
        /// <param name="t">领域对象</param>
        /// <returns>重置记录数</returns>
        int RestoreRelated(T t);

        /// <summary>
        /// 根据领域对象Id重置关联关系为空
        /// </summary>
        /// <param name="id">领域对象Id</param>
        /// <returns>重置记录数</returns>
        int RestoreRelated(U id);

        /// <summary>
        /// 根据领域对象Id集合重置关联关系为空
        /// </summary>
        /// <param name="idList">领域对象Id集合</param>
        /// <returns>重置记录数</returns>
        int RestoreRelated(IList<U> idList);

        /// <summary>
        /// 根据领域对象及领域对象Id重置关联关系为空
        /// </summary>
        /// <param name="relatedDomain">关联领域对象</param>
        /// <param name="relatedDomainId">关联领域对象Id</param>
        /// <returns>重置记录数</returns>
        int RestoreRelated(string relatedDomain, string relatedDomainId);
        #endregion

        #region DeleteByRelated

        /// <summary>
        /// 根据关联领域对象删除关联数据
        /// </summary>
        /// <param name="relatedDomain">关联领域对象</param>
        /// <param name="condition">查询条件</param>
        /// <returns>删除记录数</returns>
        int DeleteByRelated(string relatedDomain, ICondition condition = null);

        /// <summary>
        /// 根据关联领域对象及关联领域对象Id删除关联数据
        /// </summary>
        /// <param name="relatedDomain">关联领域对象</param>
        /// <param name="relatedDomainId">关联领域对象Id</param>
        /// <param name="condition">查询条件</param>
        /// <returns>删除记录数</returns>
        int DeleteByRelated(string relatedDomain, string relatedDomainId, ICondition condition = null);

        /// <summary>
        /// 根据关联领域对象及关联领域对象Id集合删除关联数据
        /// </summary>
        /// <param name="relatedDomain">关联领域对象</param>
        /// <param name="relatedDomainIdList">关联领域对象Id集合</param>
        /// <param name="condition">查询条件</param>
        /// <returns>删除记录数</returns>
        int DeleteByRelated(string relatedDomain, IList<string> relatedDomainIdList, ICondition condition = null);
        #endregion

    }
}
