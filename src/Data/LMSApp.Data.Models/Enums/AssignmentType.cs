//System
using System.ComponentModel;

namespace LMSApp.Data.Models.Enums
{
    public enum AssignmentType
    {
        [Description("Current / Текуща")]
        Current = 0,

        [Description("Final / Финална")]
        Final = 1,

        [Description("Other / Друга")]
        Other = 2
    }
}
