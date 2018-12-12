//System
using System.ComponentModel;

namespace LMSApp.Data.Models.Enums
{
    public enum EventRole
    {
        [Description("Instructor / Преподавател")]
        Instructor = 0,

        [Description("Examiner / Изпитващ")]
        Examiner = 1,

        [Description("Observer / Наблюдател или Квестор")]
        Observer = 2,

        [Description("Desicion Maker / Дискутиращ")]
        DecisionMaker = 3,

        [Description("Party Animal / Празнуващ")]
        PartyAnimal = 4,

        [Description("Other / Друго")]
        Other = 5
            
    }
}
