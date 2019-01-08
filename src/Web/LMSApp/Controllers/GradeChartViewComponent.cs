
using LMSApp.Services.CommonInterfaces;
using LMSApp.Services.Models.Chart;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace LMSApp.Controllers
{
    public class GradeChartViewComponent : ViewComponent
    {

        private IStudentAssignmentService studentAssignmentService;

        public GradeChartViewComponent(IStudentAssignmentService studentAssignmentService)
        {
            this.studentAssignmentService = studentAssignmentService;
        }

        public IViewComponentResult Invoke(string asgnId)
        {
            var studentAssignments = this.studentAssignmentService.GetStudentAssignmentsById(asgnId);

            ViewData["studentsWithAssignment"] = studentAssignments.Count().ToString();

            var binCounts = new int[5] { 0, 0, 0, 0, 0 };

            var gradedStudents = 0;

            foreach (var studentAssignment in studentAssignments)
            {
                if (studentAssignment.Grade > 0m)
                {
                    gradedStudents++;
                    var percent = studentAssignment.Grade / 45;

                    if (percent < 0.2m)
                    {
                        binCounts[0]++;
                    }
                    else if (percent < 0.4m)
                    {
                        binCounts[1]++;
                    }
                    else if (percent < 0.6m)
                    {
                        binCounts[2]++;
                    }
                    else if (percent < 0.8m)
                    {
                        binCounts[3]++;
                    }
                    else
                    {
                        binCounts[4]++;
                    }
                }
            }

            ViewData["gradedStudents"] = gradedStudents.ToString();

            var lstModel = new List<SimpleReportViewModel>();
            var percentage = 0;

            for (int i = 0; i < binCounts.Length; i++)
            {
                percentage += 20;
                lstModel.Add(new SimpleReportViewModel
                {
                    DimensionOne = $"{percentage} %",
                    Quantity = binCounts[i]
                });
            }

            return View("ViewStats", lstModel);
        }
    }
}
