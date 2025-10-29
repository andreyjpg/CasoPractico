using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using TaskModel = MinimalAPI.Data.Models.Task;

namespace MVC.Controllers
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
    }
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClientMinimal;
        private readonly HttpClient _httpClientAPI;



        public HomeController(IHttpClientFactory factory)
        {
            _httpClientMinimal = factory.CreateClient("MinimalAPI");
            _httpClientAPI = factory.CreateClient("API");

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tickets = await _httpClientMinimal.GetFromJsonAsync<List<TaskModel>>("api/task");
            return View(tickets);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> AddTask([FromBody] TaskModel newModel)
        {
            if (newModel == null)
            {
                return View();
            }
            //var model = JsonSerializer.Deserialize<TaskModel>(newModel);
            try
            {
                var response = await _httpClientAPI.PostAsJsonAsync("api/Task", newModel);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<bool>();
                    return new ApiResponse { Message = "Task created!", Success = result };
                }
                else
                {
                    return new ApiResponse { Message = "Task creation failed!", Success = false };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
