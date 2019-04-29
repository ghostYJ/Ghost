using System;
using System.Collections.Generic;
using System.Linq;

using Ghost.Utility;
namespace Ghost.Login.Domain
{
    /// <summary>
    /// 打分表信息
    /// </summary>
    public class KPIInfo
    {
        /// <summary>
        /// 提交人Id
        /// </summary>
        public int ApplyUserId { get; set; }

        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime ApplyTime { get; set; }

        /// <summary>
        /// 单位Id
        /// </summary>
        public int SupplierId { get; set; }

        #region 打分详情Xml
        private string _testKPIInfoItemXml;

        /// <summary>
        /// 打分详情Xml
        /// </summary>
        public string TestKPIInfoItemXml
        {
            get { return _testKPIInfoItemXml; }
            set
            {
                _testKPIInfoItemXml = value;

                int index = 0;
                IList<string> infoItems = _testKPIInfoItemXml.SplitString(",", true);
                if (_testKPIInfoItems == null)
                    _testKPIInfoItems = new TestKPIInfoItem();
                foreach (var infoItem in _testKPIInfoItems.GetType().GetProperties())
                {
                    if (index >= infoItems.Count)
                        break;
                    infoItem.SetValue(_testKPIInfoItems, infoItems[index].ToInt32Req(), null);
                    index++;
                }
            }
        }

        public  TestKPIInfoItem _testKPIInfoItems;

        /// <summary>
        /// 打分详情实例
        /// </summary>
        public TestKPIInfoItem TestKPIInfoItems
        {
            get { return _testKPIInfoItems; }
            set
            {
                _testKPIInfoItems = value;
                _testKPIInfoItemXml = _testKPIInfoItems.GetType().GetProperties()
                    .Select(s => s.GetValue(_testKPIInfoItems, null).ToString()).JoinToString(",", false, true);
            }
        }

        #endregion

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }


        /// <summary>
        /// 类索引器
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public string this[string key] {
            get { return ""; }
            set {; }
        }
    }


    public class TestKPIInfoItem
    {
        public int KPI_1 { get; set; }
        public int KPI_2 { get; set; }
        public int KPI_3 { get; set; }
        public int KPI_4 { get; set; }
        public int KPI_5 { get; set; }
        public int KPI_6 { get; set; }

    }

}
