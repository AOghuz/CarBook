using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using CarBook.Dto.LocationDtos;

namespace CarBook.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("Admin/AdminLocation")]
    public class AdminLocationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AdminLocationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private HttpClient CreateApiClientWithAuth()
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "carbooktoken")?.Value;
            var client = _httpClientFactory.CreateClient();
            if (!string.IsNullOrWhiteSpace(token))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return client;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var client = CreateApiClientWithAuth();
            var responseMessage = await client.GetAsync("https://localhost:7268/api/Locations");

            if (!responseMessage.IsSuccessStatusCode)
            {
                if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                    return RedirectToAction("Login", "Account", new { area = "" });
                return View(new List<ResultLocationDto>());
            }

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultLocationDto>>(jsonData) ?? new List<ResultLocationDto>();
            return View(values);
        }

        [HttpGet]
        [Route("CreateLocation")]
        public IActionResult CreateLocation()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateLocation")]
        public async Task<IActionResult> CreateLocation(CreateLocationDto createLocationDto)
        {
            var client = CreateApiClientWithAuth();

            var jsonData = JsonConvert.SerializeObject(createLocationDto);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync("https://localhost:7268/api/Locations", stringContent);
            if (responseMessage.IsSuccessStatusCode)
                return RedirectToAction("Index", "AdminLocation", new { area = "Admin" });

            ModelState.AddModelError(string.Empty, "Lokasyon oluşturulamadı.");
            return View(createLocationDto);
        }

        [Route("RemoveLocation/{id}")]
        public async Task<IActionResult> RemoveLocation(int id)
        {
            var client = CreateApiClientWithAuth();
            var responseMessage = await client.DeleteAsync($"https://localhost:7268/api/Locations?id={id}");
            if (responseMessage.IsSuccessStatusCode)
                return RedirectToAction("Index", "AdminLocation", new { area = "Admin" });

            // Hata durumunda Index'e dön
            TempData["error"] = "Silme işlemi başarısız.";
            return RedirectToAction("Index", "AdminLocation", new { area = "Admin" });
        }

        [HttpGet]
        [Route("UpdateLocation/{id}")]
        public async Task<IActionResult> UpdateLocation(int id)
        {
            var client = CreateApiClientWithAuth();
            var responseMessage = await client.GetAsync($"https://localhost:7268/api/Locations/{id}");
            if (!responseMessage.IsSuccessStatusCode)
                return RedirectToAction("Index", "AdminLocation", new { area = "Admin" });

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<UpdateLocationDto>(jsonData);
            return View(values);
        }

        [HttpPost]
        [Route("UpdateLocation/{id}")]
        public async Task<IActionResult> UpdateLocation(int id, UpdateLocationDto updateLocationDto)
        {
            var client = CreateApiClientWithAuth();

            // Hidden'dan gelmediyse id'yi set et
            if (updateLocationDto.LocationID == 0)
                updateLocationDto.LocationID = id;

            var jsonData = JsonConvert.SerializeObject(updateLocationDto);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // API'nizde [HttpPut("{id:int}")] ise:
            var responseMessage = await client.PutAsync("https://localhost:7268/api/Locations", stringContent);

            // Eğer API'nizde [HttpPut] sadece body ile id bekliyorsa yukarıdaki yerine şu kullanılmalı:
            // var responseMessage = await client.PutAsync("https://localhost:7268/api/Locations", stringContent);

            if (responseMessage.IsSuccessStatusCode)
                return RedirectToAction("Index", "AdminLocation", new { area = "Admin" });

            ModelState.AddModelError(string.Empty, "Güncelleme işlemi başarısız.");
            return View(updateLocationDto);
        }
    }
}
