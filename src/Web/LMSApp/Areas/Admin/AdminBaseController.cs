
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMSApp.Areas.Admin
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminBaseController : Controller
    {
    }
}
