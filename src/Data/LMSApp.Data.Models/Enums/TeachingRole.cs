//System
using System.ComponentModel;

namespace LMSApp.Data.Models.Enums
{
    public enum TeachingRole
    {
        [Description("Lecturer / Лектор")]
        Lecturer = 0,

        [Description("Assistant / Асистент")]
        Assistant = 1,

        [Description("Other / Друга")]
        Other = 2
    }
}
