
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMSApp.Areas.Educator
{
    [Area("Educator")]
    [Authorize(Roles = "Educator,Admin")]
    public class EducatorBaseController : Controller
    {
    }
}
