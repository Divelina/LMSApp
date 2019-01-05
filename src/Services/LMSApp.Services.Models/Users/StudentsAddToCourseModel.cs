using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LMSApp.Services.Models.Users
{
    public class StudentsAddToCourseModel
    {
        public string CourseId { get; set; }

        public string LectureciseId { get; set; }

        public IList<StudentListViewModel> StudentsInfo { get; set; }
    }
}
