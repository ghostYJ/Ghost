using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Collections.Specialized;

using Ghost.CRUD.Domain;
using Ghost.CRUD.IDao;

using IBatisNet.DataMapper;
namespace Ghost.CRUD.Dao
{
    /// <summary>
    /// 查询Dao基类
    /// </summary>
    /// <typeparam name="T">领域对象类型</typeparam>
    /// <typeparam name="U">领域对象Id类型</typeparam>
    public abstract class SelectDaoBase<T, U> : ISelectDaoBase<T, U> where T : DomainBase<U>
    {
        /// <summary>
        /// sqlMapper
        /// </summary>
        protected ISqlMapper _sqlMapper;

        /// <summary>
        /// 具体数据库的DaoHelper
        /// 通过属性注入，装饰符只能为public,不能为protected
        /// </summary>
        public IDaoHelper DaoHelper { get; set; }

        private string _sqlMapNamespace;

        /// <summary>
        /// SqlMap的Namespace
        /// </summary>
        protected virtual string SqlMapNamespace
        {
            get
            {
                if (string.IsNullOrEmpty(_sqlMapNamespace))
                    _sqlMapNamespace = typeof(T).GetDomainName();
                return _sqlMapNamespace;
            }
        }

        /// <summary>
        /// 构造带Namespace的SqlMap Statement Id
        /// </summary>
        /// <param name="statementId"></param>
        /// <returns></returns>
        protected virtual string GetStatementIdWithNamespace(string statementId)
        {
            return SqlMapNamespace + "." + statementId;
        }

        public SelectDaoBase(ISqlMapper sqlMapper)
        {
            _sqlMapper = sqlMapper;
        }

        #region Condition

        /// <summary>
        /// 根据查询条件类生成查询Hashtable
        /// </summary>
        /// <param name="condition">查询条件类</param>
        /// <returns></returns>
        public virtual Hashtable CreateConditionHashtable(ICondition condition)
        {
            return new Hashtable();
        }
        #endregion

        #region OrderProperty

        /// <summary>
        /// 默认排序条件集合，由继承类实现，当没有排序条件时默认使用此排序
        /// </summary>
        protected virtual NameValueCollection DefaultOrderCollection { get { return null; } }

        /// <summary>
        /// 排序属性和数据库字段映射
        /// </summary>
        protected IDictionary<string, string> _orderPropertyColumnMap = new Dictionary<string, string>();

        /// <summary>
        /// 获取排序属性
        /// </summary>
        /// <returns>返回排序属性列表</returns>
        public IList<string> GetOrderProperty()
        {
            return _orderPropertyColumnMap.Select(s => s.Key).ToList();
        }

        /// <summary>
        /// 添加排序属性到映射表
        /// </summary>
        /// <param name="propertyName">排序属性</param>
        /// <param name="columName">数据库字段名</param>
        protected void AddOrderPropertyColumnMap(string propertyName, string columName)
        {
            if (_orderPropertyColumnMap.ContainsKey(propertyName))
                _orderPropertyColumnMap.Add(propertyName, columName);
        }

        /// <summary>
        /// 移除排序映射表内指定的排序属性
        /// </summary>
        /// <param name="propertyName">排序属性</param>
        /// <returns>是否移除成功</returns>
        protected bool RemoveOrderPropertyColumnMap(string propertyName)
        {
            if (_orderPropertyColumnMap.ContainsKey(propertyName))
                return _orderPropertyColumnMap.Remove(propertyName);
            return false;
        }

        /// <summary>
        /// 生成排序SQL语句
        /// </summary>
        /// <param name="orderProperty">排序集合</param>
        /// <returns>生成排序Sql语句</returns>
        protected string CreateOrderSql(NameValueCollection orderProperty)
        {
            return DaoHelper.CreateOrderSql(orderProperty, _orderPropertyColumnMap);
        }

        /// <summary>
        /// 添加排序SQL到Hashtable条件中
        /// </summary>
        /// <param name="condtion"></param>
        /// <param name="orderColl"></param>
        protected void AddOrderPropertyCondition(Hashtable condtion, NameValueCollection orderColl)
        {
            string orderSql = DaoHelper.CreateOrderSql(orderColl, _orderPropertyColumnMap);
            if (string.IsNullOrEmpty(orderSql))
                orderSql = DaoHelper.CreateOrderSql(DefaultOrderCollection, _orderPropertyColumnMap);

            if (string.IsNullOrEmpty(orderSql))
                return;
            if (condtion == null)
                condtion = new Hashtable();

            if (condtion.ContainsKey(SqlMapConstants.OrderPropertyParam))
                throw new DaoException("Dao查询条件Hashtable中不能定义键名为" + SqlMapConstants.OrderPropertyParam + "，与查询条件键字冲突。");

            condtion.Add(SqlMapConstants.OrderPropertyParam, orderSql);

        }

        /// <summary>
        /// 清空排序字段映射表
        /// </summary>
        protected void ClearOrderColumnMap()
        {
            _orderPropertyColumnMap.Clear();
        }
        #endregion

        #region Select

        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="condition">查询条件，如为null或为空则查询所有</param>
        /// <param name="orderPropertyColl"></param>
        /// <returns></returns>
        protected virtual IList<T> Select(Hashtable condition, NameValueCollection orderPropertyColl = null)
        {
            AddOrderPropertyCondition(condition, orderPropertyColl);
            Hashtable ht = DaoHelper.ProcessConditionHashtable(condition);

            return _sqlMapper.QueryForList<T>(GetStatementIdWithNamespace(SqlMapConstants.SelectStatementId), ht);
        }

        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="condition">查询条件，禁止传递空Condition查询</param>
        /// <param name="orderPropertyColl">排序条件</param>
        /// <returns></returns>
        public IList<T> Select(ICondition condition, NameValueCollection orderPropertyColl = null)
        {
            Hashtable ht = CreateConditionHashtable(condition);
            if (ht.Count == 0)
                throw new DaoException("不允许使用空查询条件调用“Select”方法，如需查询所有数据请显式调用“SelectAll”方法。");

            return Select(ht, orderPropertyColl);
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <param name="orderPropertyColl">排序条件</param>
        /// <returns>查询结果</returns>
        public IList<T> SelectAll(NameValueCollection orderPropertyColl = null)
        {
            return Select(new Hashtable(), orderPropertyColl);
        }

        /// <summary>
        /// 根据条件查询顶部指定数量的数据，此方法使用SELECT TOP语句进行查询
        /// </summary>
        /// <param name="topCount">查询数量</param>
        /// <param name="condition">查询条件，如为null或空则查询所有</param>
        /// <param name="orderPropertyColl">排序条件</param>
        /// <returns>查询结果</returns>
        protected virtual IList<T> SelectTop(int topCount, Hashtable condition, NameValueCollection orderPropertyColl)
        {
            if (condition == null)
                condition = new Hashtable();
            if (condition.ContainsKey(SqlMapConstants.TopCountParam))
                throw new DaoException("查询条件Hashtable中不能定义名称为" + SqlMapConstants.TopCountParam + "的Key。");
            condition.Add(SqlMapConstants.TopCountParam, topCount);

            AddOrderPropertyCondition(condition, orderPropertyColl);
            Hashtable ht = DaoHelper.ProcessConditionHashtable(condition);

            return _sqlMapper.QueryForList<T>(GetStatementIdWithNamespace(SqlMapConstants.SelectTopStatementId), ht);
        }

        /// <summary>
        /// 根据条件查询顶部指定数量的数据
        /// </summary>
        /// <param name="topCount">查询数量</param>
        /// <param name="condition">查询条件，如为null或未设置查询条件则查询所有</param>
        /// <param name="orderPropertyColl">排序条件</param>
        /// <returns>查询结果</returns>
        public IList<T> SelectTop(int topCount, ICondition condition = null, NameValueCollection orderPropertyColl = null)
        {
            return SelectTop(topCount, CreateConditionHashtable(condition), orderPropertyColl);
        }

        public T SelectTop1(ICondition condition = null, NameValueCollection orderPropertyColl = null)
        {
            return SelectTop(1, condition, orderPropertyColl).FirstOrDefault();
        }
        #endregion

        #region SelectById
        /// <summary>
        /// 根据领域对象Id查询(虚方法，可重写)
        /// </summary>
        /// <param name="id">领域对象Id</param>
        /// <returns>查询结果</returns>
        public virtual T SelectById(U id)
        {
            if (id == null)
                return null;
            Hashtable ht = new Hashtable() { { SqlMapConstants.IdParam, id } };

            return _sqlMapper.QueryForObject<T>(GetStatementIdWithNamespace(SqlMapConstants.SelectStatementId), ht);
        }

        /// <summary>
        /// 根据领域对象Id查询，如无数据抛出异常。
        /// </summary>
        /// <param name="id">领域对象Id</param>
        /// <returns>查询结果</returns>
        public T SelectByIdReq(U id)
        {
            T t = SelectById(id);
            if (t == null)
                throw new DaoException(string.Format("{0}中没有找到Id为{1}的记录。", typeof(T).GetDomainName(), id));
            return t;
        }

        /// <summary>
        /// 根据领域对象Id集合查询，如集合为null或空则不进行查询。
        /// </summary>
        /// <param name="idList">领域对象Id集合</param>
        /// <returns>查询结果</returns>
        public IList<T> SelectByIdList(IList<U> idList)
        {
            if (idList == null || idList.Count == 0)
                return new List<T>();

            return Select(new Hashtable() { { SqlMapConstants.IdsParam, idList } });
        }

        #endregion

        #region SelectCount

        /// <summary>
        /// 根据条件类查询数量
        /// </summary>
        /// <param name="condition">查询条件，如为null或空则查询所有</param>
        /// <returns>返回数量结果</returns>
        protected virtual int SelectCount(Hashtable condition = null)
        {
            Hashtable ht = DaoHelper.ProcessConditionHashtable(condition);

            return (int)_sqlMapper.QueryForObject(GetStatementIdWithNamespace(SqlMapConstants.SelectCountStatementId), ht);
        }

        /// <summary>
        /// 根据条件类查询数量
        /// </summary>
        /// <param name="condition">查询条件，如为null或空则查询所有</param>
        /// <returns>返回数量结果</returns>
        public int SelectCount(ICondition condition = null)
        {
            return SelectCount(CreateConditionHashtable(condition));
        }

        /// <summary>
        /// 查询所有数据数量
        /// </summary>
        /// <returns>返回数量结果</returns>
        public int SelectAllCount()
        {
            return SelectCount(new Hashtable());
        }
        #endregion

        #region SelectByPage

        /// <summary>
        /// 根据条件分页查询(虚方法，可重写)
        /// </summary>
        /// <param name="startRecordIndex">当前页数据记录的起始索引，从1开始</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="condition">查询条件，如为null或未设置查询条件则查询所有</param>
        /// <param name="orderPropertyColl">排序条件</param>
        /// <returns>查询结果</returns>
        public virtual IList<T> SelectByPage(int startRecordIndex, int pageSize, Hashtable condition = null, NameValueCollection orderPropertyColl = null)
        {
            AddOrderPropertyCondition(condition, orderPropertyColl);
            Hashtable ht = DaoHelper.ProcessConditionHashtable(condition);

            DaoHelper.SetPageArg(ref ht, startRecordIndex, pageSize);

            return _sqlMapper.QueryForList<T>(GetStatementIdWithNamespace(SqlMapConstants.SelectByPageStatementId), ht);
        }

        /// <summary>
        /// 根据条件分页查询
        /// </summary>
        /// <param name="startRecordIndex">当前页数据记录的起始索引，从1开始</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="condition">查询条件，如为null或未设置查询条件则查询所</param>
        /// <param name="orderPropertyColl">排序条件</param>
        /// <returns>查询结果</returns>
        public IList<T> SelectByPage(int startRecordIndex, int pageSize, ICondition condition = null, NameValueCollection orderPropertyColl = null)
        {
            return SelectByPage(startRecordIndex, pageSize
                         , CreateConditionHashtable(condition), orderPropertyColl);
        }

        /// <summary>
        /// 分页查询所有数据
        /// </summary>
        /// <param name="startRecordIndex">当前页数据记录的起始索引，从1开始</param>
        /// <param name="pageSize">每页显示的记录数</param>
        /// <param name="orderPropertyColl">排序条件</param>
        /// <returns>查询结果</returns>
        public IList<T> SelectAllByPage(int startRecordIndex, int pageSize, NameValueCollection orderPropertyColl = null)
        {
            return SelectByPage(startRecordIndex, pageSize, new Hashtable(), orderPropertyColl);
        }

        #endregion
    }
}
