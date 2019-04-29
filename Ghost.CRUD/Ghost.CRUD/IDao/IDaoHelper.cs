using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Ghost.CRUD.IDao
{
    /// <summary>
    /// Dao帮助接口
    /// </summary>
    public interface IDaoHelper
    {
        /// <summary>
        /// 根据排序项集合和字段对照表生成排序语句
        /// </summary>
        /// <param name="coll">排序集合</param>
        /// <param name="map">排序属性和数据库字段映射</param>
        /// <returns>返回生成排序SQL语句</returns>
        string CreateOrderSql(NameValueCollection coll, IDictionary<string, string> map);

        /// <summary>
        /// 对查询条件中的参数进行安全转换
        /// </summary>
        /// <param name="cond"></param>
        /// <returns></returns>
        Hashtable ProcessConditionHashtable(Hashtable cond);

        /// <summary>
        /// 转换LIKE查询参数中的通配符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        string ProcessLike(string str);

        /// <summary>
        /// LIKE参数化查询通配符转义
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        string ProcessLikeParam(string str);

        /// <summary>
        /// 为集合中添加分页参数，参数必须使用ref修饰符，否则当参数为null时，cond = new Hashtable()并不会替换实参cond
        /// </summary>
        /// <param name="cond"></param>
        /// <param name="startRecordIndex"></param>
        /// <param name="pageSize"></param>
        void SetPageArg(ref Hashtable cond, int startRecordIndex, int pageSize);
    }
}
