//System
using System.ComponentModel;

namespace LMSApp.Data.Models.Enums
{
    public enum TeachingRole
    {
        [Description("Lecturer / Лектор")]
        Lecturer,

        [Description("Assistant / Асистент")]
        Assistant,

        [Description("Other / Друга")]
        Other
    }
}
