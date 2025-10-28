using TaskModel = MinimalAPI.Data.Models.Task;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using System.Diagnostics;
using System.Net.Http;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;


        public HomeController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("MinimalAPI");
        }

        public async Task<IActionResult> Index()
        {
            var tickets = await _httpClient.GetFromJsonAsync<List<TaskModel>>("api/tickets");
            return View(tickets);
        }

    }
}
