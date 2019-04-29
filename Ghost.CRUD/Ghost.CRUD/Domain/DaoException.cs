using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ghost.CRUD.Domain
{
    /// <summary>
    /// Dao异常类
    /// </summary>
    public class DaoException : Exception
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DaoException() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        public DaoException(string message) : base(message) { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public DaoException(string message, Exception innerException) : base(message, innerException) { }
    }
}
