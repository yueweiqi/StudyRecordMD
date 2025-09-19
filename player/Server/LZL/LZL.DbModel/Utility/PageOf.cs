using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZL.DbModel.Utility
{
    public record PageDataOf
    {
        /// <summary>
        /// 每页数量  默认20
        /// </summary>
        public int PageSize { get; set; } = 20;
        /// <summary>
        /// 当前页 默认0,0为首页
        /// </summary>
        public int PageIndex { get; set; } = 0;
    }

    public record PageDataOf<T> : PageDataOf
    {
        /// <summary>
        /// 数据集合
        /// </summary>
        public List<T> Items { get; set; }
        /// <summary>
        /// 数据总数
        /// </summary>
        public int TotalCount { get; set; } = 0;
    }
}
