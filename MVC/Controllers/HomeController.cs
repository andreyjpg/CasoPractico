using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using TaskModel = MinimalAPI.Data.Models.Task;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClientMinimal;
        private readonly HttpClient _httpClientAPI;
        private readonly HomeViewModel _homeViewModel;



        public HomeController(IHttpClientFactory factory)
        {
            _httpClientMinimal = factory.CreateClient("MinimalAPI");
            _httpClientAPI = factory.CreateClient("API");
            _homeViewModel = new HomeViewModel();

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tickets = await _httpClientMinimal.GetFromJsonAsync<List<TaskModel>>("api/task");
            _homeViewModel.Tickets = tickets;
            return View(_homeViewModel);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> AddTask(HomeViewModel newModel)
        {
            if (newModel == null)
            {
                return View();
            }
            newModel.NewTask.CreatedAt = DateTime.Now;
            try
            {
                var json = JsonSerializer.Serialize(newModel.NewTask);  
                var response = await _httpClientAPI.PostAsJsonAsync("api/Task", json);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<bool>();
                    _homeViewModel.ApiResponse = new ApiResponse { Message = "Task created!", Success = result };
                }
                else
                {
                    _homeViewModel.ApiResponse = new ApiResponse { Message = "Task creation failed!", Success = false };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index", "Home", _homeViewModel);
        }

    }
}
