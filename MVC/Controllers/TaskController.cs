using Microsoft.AspNetCore.Mvc;

namespace MinimalAPI.MVC.Controllers
{
    public class TaskController : Controller
    {
        public IActionResult Approvals()
        {

            var roleName = TempData["RoleName"]?.ToString();
            if (TempData.ContainsKey("RoleName"))
            {
                TempData.Keep("RoleName");
            }

            if (!(string.Equals(roleName, "Admin", StringComparison.OrdinalIgnoreCase) ||
                  string.Equals(roleName, "Manager", StringComparison.OrdinalIgnoreCase)))
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}
