
namespace LMSApp.Data.Models
{
    using LMSApp.Data.Common;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class WeekTime : BaseModel<int>
    {
        [Required]
        public DayOfWeek DayOfWeek { get; set; }

        [Required]
        public int StartHour { get; set; }

        public int EndHour { get; set; }
    }
}
