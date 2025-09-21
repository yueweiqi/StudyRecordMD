using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZL.DbModel.Enums
{
    public enum RankNameEnum
    {
        [Description("无分段")]
        None = 0,
        [Description("最强王者")]
        WangZhe =1,
        [Description("傲视宗师")]
        ZongShi = 2,
        [Description("超凡大师")]
        DaShi = 3,
        [Description("钻石一")]
        ZuanOne = 4,
        [Description("钻石二")]
        ZuanTwo = 5,
        [Description("钻石三")]
        ZuanThree = 6,
        [Description("钻石四")]
        ZuanFour = 7,
        [Description("翡翠一")]
        FeiCuiOne = 8,
        [Description("翡翠二")]
        FeiCuiTwo = 9,
        [Description("翡翠三")]
        FeiCuiThree = 10,
        [Description("翡翠四")]
        FeiCuiFour = 11,
        [Description("铂金一")]
         BoJinOne= 12,
        [Description("铂金二")]
        BoJinTwo = 13,
        [Description("铂金三")]
        BoJinThree = 14,
        [Description("铂金四")]
        BoJinFour = 15,
        [Description("黄金一")]
        HuangJinOne = 16,
        [Description("黄金二")]
        HuangJinTwo = 17,
        [Description("黄金三")]
        HuangJinThree = 18,
        [Description("黄金四")]
        HuangJinFour = 19,
    }
}
