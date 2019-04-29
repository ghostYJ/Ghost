using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghost.CRUD.Domain
{
    /// <summary>
    /// 领域对象泛型基类，领域对象类名不能超过50个字符
    /// </summary>
    /// <typeparam name="T">领域对象Id的类型，只能是int、long或string</typeparam>
    [Serializable]
    public abstract class DomainBase<T>
    {
        /// <summary>
        /// Id，领域对象标识，string类型的Id长度不能超过50个字符，不能有单引号（避免LIKE查询语句出错），其他特殊字符最好也不要使用
        /// </summary>
        public T Id { get; set; }
    }
}
