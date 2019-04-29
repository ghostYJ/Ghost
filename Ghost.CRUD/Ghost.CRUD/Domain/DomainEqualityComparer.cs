using System.Collections.Generic;

namespace Ghost.CRUD.Domain
{
    /// <summary>
    /// 领域对象比较器
    /// </summary>
    /// <typeparam name="T">领域对象类型</typeparam>
    /// <typeparam name="U">领域对象Id类型</typeparam>
    public class DomainEqualityComparer<T, U> : IEqualityComparer<T> where T : DomainBase<U>
    {
        /// <summary>
        /// 判断是否相等
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(T x, T y)
        {
            return x.Id.Equals(y.Id);
        }

        /// <summary>
        /// 取HashCode
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(T obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
