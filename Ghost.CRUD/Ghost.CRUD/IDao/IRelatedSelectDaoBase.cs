using Ghost.CRUD.Domain;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghost.CRUD.IDao
{
    /// <summary>
    /// 关联领域对象数据查询接口
    /// </summary>
    /// <typeparam name="T">领域对象类型</typeparam>
    /// <typeparam name="U">领域对象Id类型</typeparam>
    public interface IRelatedSelectDaoBase<T, U> : ISelectDaoBase<T, U> where T : DomainBase<U>
    {
        #region Select

        /// <summary>
        /// 查询关联领域对象
        /// </summary>
        /// <param name="relatedDomain">关联领域对象</param>
        /// <param name="condition">查询条件</param>
        /// <param name="orderPropertyColl">排序条件</param>
        /// <returns>查询结果</returns>
        IList<T> SelectRelated(string relatedDomain, ICondition condition = null, NameValueCollection orderPropertyColl = null);

        /// <summary>
        /// 查询关联领域对象
        /// </summary>
        /// <param name="relatedDomain">关联领域对象</param>
        /// <param name="relatedDomainId">关联领域对象Id</param>
        /// <param name="condition">查询条件</param>
        /// <param name="orderPropertyColl">排序条件</param>
        /// <returns>查询结果</returns>
        IList<T> SelectRelated(string relatedDomain, string relatedDomainId, ICondition condition = null,
            NameValueCollection orderPropertyColl = null);

        /// <summary>
        /// 查询关联领域对象
        /// </summary>
        /// <param name="relatedDomain">关联领域对象</param>
        /// <param name="relatedDomainIdList">关联领域对象Id集合</param>
        /// <param name="condition">查询条件</param>
        /// <param name="orderPropertyColl">排序条件</param>
        /// <returns>查询结果</returns>
        IList<T> SelectRelated(string relatedDomain, IList<string> relatedDomainIdList, ICondition condition = null, NameValueCollection orderPropertyColl = null);

        #endregion

        #region SelectCount

        /// <summary>
        /// 查询关联领域对象数量
        /// </summary>
        /// <param name="relatedDomain">关联领域对象</param>
        /// <param name="condition">查询条件</param>
        /// <returns>查询数量结果</returns>
        int SelectRelatedCount(string relatedDomain, ICondition condition = null);

        /// <summary>
        /// 查询关联领域对象数量
        /// </summary>
        /// <param name="relatedDomain">关联领域对象</param>
        /// <param name="relatedDomainId">关联领域对象Id</param>
        /// <param name="condition">查询条件</param>
        /// <returns>查询数量结果</returns>
        int SelectRelatedCount(string relatedDomain, string relatedDomainId, ICondition condition = null);

        /// <summary>
        /// 查询关联领域对象数量
        /// </summary>
        /// <param name="relatedDomain">关联领域对象</param>
        /// <param name="relatedDomainId">关联领域对象Id集合</param>
        /// <param name="condition">查询条件</param>
        /// <returns>查询数量结果</returns>
        int SelectRelatedCount(string relatedDomain, IList<string> relatedDomainId, ICondition condition = null);
        #endregion

        #region SelectByPage

        /// <summary>
        /// 分页查询关联领域对象
        /// </summary>
        /// <param name="startRecordIndex">当前页数据记录的起始索引，从1开始</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="relatedDomain">关联领域对象</param>
        /// <param name="condition">查询条件</param>
        /// <param name="orderPropertyColl">排序条件</param>
        /// <returns>查询结果</returns>
        IList<T> SelectRelatedByPage(int startRecordIndex, int pageSize, string relatedDomain, ICondition condition = null,
            NameValueCollection orderPropertyColl = null);

        /// <summary>
        /// 分页查询关联领域对象
        /// </summary>
        /// <param name="startRecordIndex">当前页数据记录的起始索引，从1开始</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="relatedDomain">关联领域对象</param>
        /// <param name="relatedDomainId">关联领域对象Id</param>
        /// <param name="condition">查询条件</param>
        /// <param name="orderPropertyColl">排序条件</param>
        /// <returns>查询结果</returns>
        IList<T> SelectRelatedByPage(int startRecordIndex, int pageSize, string relatedDomain, string relatedDomainId, ICondition condition = null, NameValueCollection orderPropertyColl = null);

        /// <summary>
        /// 分页查询关联领域对象
        /// </summary>
        /// <param name="startRecordIndex">当前页数据记录的起始索引，从1开始</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="relatedDomain">关联领域对象</param>
        /// <param name="relatedDomainIdList">关联领域对象Id集合</param>
        /// <param name="condition">查询条件</param>
        /// <param name="orderPropertyColl">排序条件</param>
        /// <returns>查询结果</returns>
        IList<T> SelectRelatedByPage(int startRecordIndex, int pageSize, string relatedDomain, IList<string> relatedDomainIdList, ICondition condition = null, NameValueCollection orderPropertyColl = null);
        #endregion
    }
}
