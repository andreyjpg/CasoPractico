using Microsoft.AspNetCore.Mvc;
using MinimalAPI.MVC.Models;
using System.Text.Json;
using System.Threading.Tasks;
using Role = MinimalAPI.Data.Models.Role;
using User = MinimalAPI.Data.Models.User;
using UserRole = MinimalAPI.Data.Models.UserRole;

namespace MinimalAPI.MVC.Controllers
{
    public class RolesController : Controller
    {
        private readonly HttpClient _httpClientAPI;
        private RolesViewModel _rolesViewModel;


        public RolesController(IHttpClientFactory factory) {
            _httpClientAPI = factory.CreateClient("API");
            _rolesViewModel = new RolesViewModel();
        }
        public async Task<IActionResult> Index()
        {
            await PopulateViewModelAsync();
            return View(_rolesViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(int userId, int roleId)
        {
            var userRole = new UserRole();
            userRole.RoleId = roleId;
            userRole.UserId = userId;
            var json = JsonSerializer.Serialize(userRole);

            var response = await _httpClientAPI.PostAsJsonAsync("api/UserRole", json);
            return RedirectToAction("Index", "Roles", _rolesViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(RolesViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.NewRoleName))
            {
                ModelState.AddModelError(nameof(model.NewRoleName), "Role name is required.");
                await PopulateViewModelAsync();
                _rolesViewModel.NewRoleName = model.NewRoleName;
                return View("Index", _rolesViewModel);
            }

            var newRole = new Role
            {
                RoleName = model.NewRoleName.Trim()
            };

            var response = await _httpClientAPI.PostAsJsonAsync("api/Role", newRole);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(nameof(model.NewRoleName), "Unable to create role. Please try again.");
                await PopulateViewModelAsync();
                _rolesViewModel.NewRoleName = model.NewRoleName;
                return View("Index", _rolesViewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateViewModelAsync()
        {
            var users = await _httpClientAPI.GetFromJsonAsync<List<User>>("api/User") ?? new List<User>();
            var roles = await _httpClientAPI.GetFromJsonAsync<List<Role>>("api/Role") ?? new List<Role>();

            _rolesViewModel.Users = users;
            _rolesViewModel.Roles = roles;
        }
    }
}
