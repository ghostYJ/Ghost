using Ghost.CRUD.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghost.CRUD.IDao
{
    /// <summary>
    /// 数据查询接口
    /// </summary>
    /// <typeparam name="T">领域对象类型</typeparam>
    /// <typeparam name="U">领域对象Id类型</typeparam>
    public interface ISelectDaoBase<T, U> where T : DomainBase<U>
    {
        #region Condition

        /// <summary>
        /// 根据查询条件类生成查询Hashtable
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        Hashtable CreateConditionHashtable(ICondition condition);
        #endregion

        #region OrderProperty

        /// <summary>
        /// 取支持的排序属性
        /// </summary>
        /// <returns></returns>
        IList<string> GetOrderProperty();
        #endregion

        #region Select

        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="condition">查询条件，禁止传递空Condition查询</param>
        /// <param name="orderPropertyColl">排序条件</param>
        /// <returns></returns>
        IList<T> Select(ICondition condition, NameValueCollection orderPropertyColl = null);

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <param name="orderPropertyColl">排序条件</param>
        /// <returns>查询结果</returns>
        IList<T> SelectAll(NameValueCollection orderPropertyColl = null);

        /// <summary>
        /// 根据条件查询顶部指定数量的数据
        /// </summary>
        /// <param name="topCount">查询数量</param>
        /// <param name="condition">查询条件，如为null或未设置查询条件则查询所有</param>
        /// <param name="orderPropertyColl">排序条件</param>
        /// <returns>返回结果</returns>
        IList<T> SelectTop(int topCount, ICondition condition = null, NameValueCollection orderPropertyColl = null);

        /// <summary>
        /// 根据条件查询顶部第一条记录
        /// </summary>
        /// <param name="condition">查询条件，如为null或未设置查询条件则查询所有</param>
        /// <param name="orderPropertyColl">排序条件</param>
        /// <returns>查询结果</returns>
        T SelectTop1(ICondition condition = null, NameValueCollection orderPropertyColl = null);
        #endregion

        #region SelectById

        /// <summary>
        /// 根据领域对象Id查询
        /// </summary>
        /// <param name="id">领域对象Id</param>
        /// <returns>查询结果</returns>
        T SelectById(U id);

        /// <summary>
        /// 根据领域对象Id查询，如无数据抛出异常。
        /// </summary>
        /// <param name="id">领域对象Id</param>
        /// <returns>查询结果</returns>
        T SelectByIdReq(U id);

        /// <summary>
        /// 根据领域对象Id集合查询，如集合为null或为空则不进行查询
        /// </summary>
        /// <param name="idList">领域对象Id集合</param>
        /// <returns>查询结果</returns>
        IList<T> SelectByIdList(IList<U> idList);
        #endregion

        #region SelectCount

        /// <summary>
        /// 根据条件类查询数量
        /// </summary>
        /// <param name="condition">查询条件，如为null或空则查询所有</param>
        /// <returns>返回数量结果</returns>
        int SelectCount(ICondition condition = null);

        /// <summary>
        /// 查询所有数据数量
        /// </summary>
        /// <returns>返回数量结果</returns>
        int SelectAllCount();
        #endregion

        #region SelectByPage

        /// <summary>
        /// 根据条件分页查询
        /// </summary>
        /// <param name="startRecordIndex">当前页数据记录的起始索引，从1开始</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="condition">查询条件，如为null或未设置查询条件则查询所有</param>
        /// <param name="orderProperty">排序条件</param>
        /// <returns>查询结果</returns>
        IList<T> SelectByPage(int startRecordIndex, int pageSize, ICondition condition = null, NameValueCollection orderPropertyColl = null);

        /// <summary>
        /// 分页查询所有数据
        /// </summary>
        /// <param name="startRecordIndex">当前页数据记录的起始索引，从1开始</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="orderPropertyColl">排序条件</param>
        /// <returns></returns>
        IList<T> SelectAllByPage(int startRecordIndex, int pageSize, NameValueCollection orderPropertyColl = null);

        #endregion
    }
}
