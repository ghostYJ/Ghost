using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghost.CRUD.Domain
{
    /// <summary>
    /// 关联领域对象基类
    /// </summary>
    /// <typeparam name="T">领域对象Id类型</typeparam>
    public abstract class RelatedDomainBase<T> : DomainBase<T>
    {
        /// <summary>
        /// 关联领域对象
        /// </summary>
        public string RelatedDomain { get; set; }

        /// <summary>
        /// 关联领域对象Id
        /// </summary>
        public string RelatedDomainId { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        protected RelatedDomainBase()
        {
            RelatedDomain = string.Empty;
            RelatedDomainId = string.Empty;
        }
    }
}
