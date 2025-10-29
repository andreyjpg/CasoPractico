using Microsoft.AspNetCore.Mvc;

namespace MinimalAPI.MVC.Controllers
{
    public class TaskController : Controller
    {
        public IActionResult Approvals()
        {
            var roleId = TempData["RoleId"] != null ? System.Convert.ToInt32(TempData["RoleId"]) : 0;
            TempData.Keep("RoleId");
            if (roleId != 1)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}
