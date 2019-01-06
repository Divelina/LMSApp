
using LMSApp.Data.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace LMSApp.Services.Models.Users
{
    public class StudentsFilterModel
    {
        public StudentsFilterModel()
        {
            this.UniIdMax = int.MaxValue;
            this.UniIdMin = 0;
        }

        public int UniIdMin { get; set; }

        public int UniIdMax { get; set; }

        public string Major { get; set; }

        public string FacultyName { get; set; }

        public int GroupNumber { get; set; }

        public string NameStr { get; set; }

        public string EmailStr { get; set; }

        public string ForAction { get; set; }

        public string ForController { get; set; }

        public string ForArea { get; set; }

        public string ParamId { get; set; }
    }
}
