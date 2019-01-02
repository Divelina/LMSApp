//App
using LMSApp.Data.Common;
//System
using System;
using System.ComponentModel.DataAnnotations;

namespace LMSApp.Data.Models
{
    //To store lecture and excercises scheduled times
    public class WeekTime : BaseModel<int>
    {
        [Required]
        public DayOfWeek DayOfWeek { get; set; }

        [Required]
        public int StartHour { get; set; }

        public int EndHour { get; set; }

        public override string ToString()
        {
            return $"{this.DayOfWeek} {this.StartHour}";
        }
    }
}
