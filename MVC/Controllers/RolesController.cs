using Microsoft.AspNetCore.Mvc;
using MinimalAPI.Data.Models;
using MinimalAPI.MVC.Models;
using MVC.Models;
using System.Text.Json;

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
            var users = await _httpClientAPI.GetFromJsonAsync<List<User>>("api/User");
            _rolesViewModel.Users = users;

            var roles = await _httpClientAPI.GetFromJsonAsync<List<Role>>("api/Role");
            _rolesViewModel.Roles = roles;
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
        public async Task<IActionResult> AddRole(RolesViewModel viewModel)
        {
            var json = JsonSerializer.Serialize(viewModel.NewRole);
            var result = await _httpClientAPI.PostAsJsonAsync("api/Role", json);
            return RedirectToAction("Index", "Roles", _rolesViewModel);

        }
    }
}
