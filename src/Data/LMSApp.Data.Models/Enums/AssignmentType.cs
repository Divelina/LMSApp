//System
using System.ComponentModel;

namespace LMSApp.Data.Models.Enums
{
    public enum AssignmentType
    {
        [Description("Current / Текуща")]
        Current,

        [Description("Final / Финална")]
        Final,

        [Description("Other / Друга")]
        Other
    }
}
