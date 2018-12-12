//System
using System.ComponentModel;

namespace LMSApp.Data.Models.Enums
{
    public enum AssignmentStatus
    {
        [Description("Unfinished / Незавършена")]
        Unfinished = 0,

        [Description("Under Revision / Проверява се")]
        UnderRevision = 1,

        [Description("To Remake / За поправяне")]
        ToRemake = 2,

        [Description("Finished / Завършена")]
        Finished = 3,

        [Description("Other / Друго")]
        Other = 4
    }
}
