using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZL.DbModel.Enums
{
    public enum PositionEnum
    {
        [Description("上单")]
        Top = 1,
        [Description("打野")]
        Jug = 2,
        [Description("中单")]
        Mid = 3,
        [Description("射手")]
        AD = 4,
        [Description("辅助")]
        SUP = 5,
        [Description("替补-上单")]
        SubTop = 6,
        [Description("替补-打野")]
        SubJug = 7,
        [Description("替补-中单")]
        SubMid = 8,
        [Description("替补-射手")]
        SubAD = 9,
        [Description("替补-辅助")]
        SubSUP = 10,
    }
}
