//System
using System.ComponentModel;

namespace LMSApp.Data.Models.Enums
{
    public enum EventRole
    {
        [Description("Instructor / Преподавател")]
        Instructor,

        [Description("Examiner / Изпитващ")]
        Examiner,

        [Description("Observer / Наблюдател или Квестор")]
        Observer,

        [Description("Desicion Maker / Дискутиращ")]
        DecisionMaker,

        [Description("Party Animal / Празнуващ")]
        PartyAnimal,

        [Description("Other / Друго")]
        Other
            
    }
}
