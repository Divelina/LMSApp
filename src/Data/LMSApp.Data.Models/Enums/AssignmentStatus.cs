//System
using System.ComponentModel;

namespace LMSApp.Data.Models.Enums
{
    public enum AssignmentStatus
    {
        [Description("Unfinished / Незавършена")]
        Unfinished,

        [Description("Under Revision / Проверява се")]
        UnderRevision,

        [Description("To Remake / За поправяне")]
        ToRemake,

        [Description("Finished / Завършена")]
        Finished,

        [Description("Other / Друго")]
        Other
    }
}
