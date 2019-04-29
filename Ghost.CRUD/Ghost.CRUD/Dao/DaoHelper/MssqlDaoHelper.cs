using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

using Ghost.CRUD.IDao;

namespace Ghost.CRUD.Dao
{
    public class MssqlDaoHelper : IDaoHelper
    {
        /// <summary>
        /// 根据排序属性集合和字段对照表生成排序语句
        /// </summary>
        /// <param name="coll">排序集合</param>
        /// <param name="map">排序属性和数据库字段映射</param>
        /// <returns></returns>
        public string CreateOrderSql(NameValueCollection coll, IDictionary<string, string> map)
        {
            if (coll == null || coll.Count == 0 || map == null || map.Count == 0)
                return string.Empty;
            StringBuilder sb = new StringBuilder();

            foreach (string s in coll)
            {
                if (map.ContainsKey(s))
                {
                    if (sb.Length > 0)
                        sb.Append(", ");
                    sb.Append(map[s]);
                    if (coll[s].ToUpper().Trim() == "DESC")
                        sb.Append(" DESC");
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 出于安全考虑，进行Sql值转换
        /// </summary>
        /// <param name="cond"></param>
        /// <returns></returns>
        public Hashtable ProcessConditionHashtable(Hashtable cond)
        {
            if (cond == null)
                return null;
            Hashtable ht = new Hashtable();
            foreach (DictionaryEntry entry in cond)
            {
                string key = entry.Key.ToString();
                if (key.EndsWith("_MustIn"))
                {
                    var obj = entry.Value;
                    if (obj == null || !(obj is ICollection) || ((ICollection)obj).Count == 0)
                        throw new ArgumentNullException(key, "未为集合参数指定值。");
                    ht.Add(key, entry.Value);
                    continue;
                }

                if (key.EndsWith("_Contain") || key.EndsWith("_NotContain")
                    || key.EndsWith("_BeginWith") || key.EndsWith("_EndWith"))
                {
                    string v = ProcessLike(entry.Value.ToString());
                    //过滤?，IBatis中会自动将?识别为匿名参数，导致异常：System.ArgumentOutOfRangeException : 指定的参数已超出有效值的范围。
                    v = v.Replace("?", "");
                    ht.Add(key, v);
                    continue;
                }

                //参数化查询时，%号无法通过拼接字符串方式查询，只能在此处设置
                //参数化查询，参数化查询时仍需转义
                if (key.EndsWith("_ContainParam") || key.EndsWith("_NotContainParam"))
                {
                    ht.Add(key, "%" + ProcessLikeParam(entry.Value.ToString()) + "%");
                    continue;
                }

                if (key.EndsWith("_BeginWithParam"))
                {
                    ht.Add(key, ProcessLikeParam(entry.Value.ToString()) + "%");
                    continue;
                }

                if (key.EndsWith("_EndWithParam"))
                {
                    ht.Add(key, "%" + ProcessLikeParam(entry.Value.ToString()));
                    continue;
                }

                ht.Add(key, entry.Value);
            }
            return ht;
        }

        /// <summary>
        /// LIKE拼接SQL查询通配符转义，拼接赋值时只需替换“'”
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string ProcessLike(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            str = str.Replace("[", "[[]");//【[】必须为第一个，否则转义符会再次转义
            str = str.Replace("_", "[_]");//或将【_】替换成【\_】
            str = str.Replace("%", "[%]");//或将【%】替换成【\%】
            str = str.Replace("'", "''");
            return str;
        }

        /// <summary>
        /// LIKE参数化查询通配符转义
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string ProcessLikeParam(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            str = str.Replace("[", "[[]");  //[必须为第一个，否则转义符会再次被转义
            str = str.Replace("_", "[_]");  //替换成\_也可
            str = str.Replace("%", "[%]");  //替换成\%也可
            //^                             //^无需转义
            //str = str.Replace("'", "''"); //参数化查询时'会自动转义，此处再替换的化会变成四个单引号

            return str;
        }

        /// <summary>
        /// 为Mssql数据库查询参数集合中添加分页参数，一定是ref，否则当参数为null时，cond = new Hashtable()不会替换实参cond
        /// </summary>
        /// <param name="cond"></param>
        /// <param name="startRecordIndex"></param>
        /// <param name="pageSize"></param>
        public void SetPageArg(ref Hashtable cond, int startRecordIndex, int pageSize)
        {
            if (cond == null)
                cond = new Hashtable();
            if (cond.Contains("NotInSize"))
                cond["NotInSize"] = startRecordIndex - 1;
            else
                cond.Add("NotInSize", startRecordIndex - 1);
            if (cond.Contains("PageSize"))
                cond["PageSize"] = pageSize;
            else
                cond.Add("PageSize", pageSize);
        }
    }
}
