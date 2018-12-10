
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMSApp.Areas.Student
{
    [Area("Student")]
    [Authorize(Roles = "Student")]
    public class StudentBaseController : Controller
    {
    }
}
