using Ghost.CRUD.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghost.CRUD.IDao
{
   public interface IDaoBase<T,U>:ISelectDaoBase<T,U> where T :DomainBase<U>
    {
        #region Insert Update Delete Truncate

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="t">领域对象</param>
        /// <returns>领域对象Id</returns>
        U Insert(T t);

        /// <summary>
        /// 更新，根据领域对象Id更新
        /// </summary>
        /// <param name="t">领域对象</param>
        /// <returns>更新数量</returns>
        int Update(T t);

        /// <summary>
        /// 更新，根据指定领域对象Id更新
        /// </summary>
        /// <param name="id">领域对象Id</param>
        /// <param name="t">领域对象</param>
        /// <returns>更新数量</returns>
        int Update(U id, T t);

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="condition">删除条件，如为null或未设置查询条件则不进行删除</param>
        /// <returns>删除数量</returns>
        int Delete(ICondition condition);

        /// <summary>
        /// 删除所有数据，数据量大时建议调用Truncate方法
        /// </summary>
        /// <returns></returns>
        int DeleteAll();

        /// <summary>
        /// 根据Id删除
        /// </summary>
        /// <param name="id">领域对象Id</param>
        /// <returns>删除结果，实际删除数量可能大于1</returns>
        int DeleteById(U id);

        /// <summary>
        /// 根据Id集合删除
        /// </summary>
        /// <param name="idList">领域对象Id集合，如集合为null或空则不进行删除</param>
        /// <returns>删除数量</returns>
        int DeleteByIdList(IList<U> idList);

        /// <summary>
        /// 清空表，不记录日志
        /// 清空数据表时使用此方法性能好且节省空间
        /// </summary>
        void Truncate();
        #endregion

    }
}
