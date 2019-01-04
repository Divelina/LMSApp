//App
using LMSApp.Data.Common;
//System
using System;
using System.ComponentModel.DataAnnotations;

namespace LMSApp.Data.Models
{
    //To store lecture and excercises scheduled times
    public class WeekTime : BaseDeletableModel<int>
    {
        [Required]
        public DayOfWeek DayOfWeek { get; set; }

        [Required]
        public string StartHour { get; set; }

        public string EndHour { get; set; }

        public override string ToString()
        {
            var weekTimeFormat = $"{this.DayOfWeek} {this.StartHour}";

            if (this.EndHour != null)
            {
                weekTimeFormat += $" - { this.EndHour}";
            }

            return weekTimeFormat;
        }
    }
}
