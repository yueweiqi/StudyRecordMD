using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZL.DbModel.Enums
{
    public enum PlayerIdentityEnum
    {
        [Description("队员")]
        None=0,
        [Description("队长")]
        Leader=1
    }
}
