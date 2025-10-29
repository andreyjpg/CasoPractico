using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MinimalAPI.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace MinimalAPI.MVC.Controllers
{
    public class LoginInputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }

    public class LoginResponse
    {
        public string? Message { get; set; }
        public User? Data { get; set; }
    }

    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;


        public LoginController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("MinimalAPI");
        }

        [HttpGet]
        public IActionResult Index() => View(new LoginInputModel());

        [HttpPost]
        public async Task<IActionResult> Index(LoginInputModel credentials)
        { 
            if (credentials.Email.IsNullOrEmpty() || credentials.Password.IsNullOrEmpty())
            {
                return View(credentials);
            }
            if (!ModelState.IsValid)
                return View(credentials);

            var user = await _httpClient.PostAsJsonAsync("/login", JsonSerializer.Serialize(credentials));
            if (user == null)
            {
                throw new Exception("Email or Password are incorrect");
            }
            var response = await user.Content.ReadFromJsonAsync<LoginResponse>();
            TempData["UserId"] = response.Data.UserId;
            TempData["UserName"] = response.Data.FullName;
            TempData["RoleId"] = response.Data.UserRoles.FirstOrDefault().RoleId;
            TempData["Email"] = response.Data.Email;
            

            return RedirectToAction("Index", "Home");
        }
    }

}