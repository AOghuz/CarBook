using CarBook.Dto.LocationDtos;
using CarBook.Dto.ReservationDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using System.Net;                       // <-- eklendi
using System.Net.Http.Headers;          // <-- eklendi
using CarBook.Dto.TestimonialDtos;

namespace CarBook.WebUI.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ReservationController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.v1 = "Araç Kiralama";
            ViewBag.v2 = "Araç Rezervasyon Formu";
            ViewBag.v3 = id;

            var client = _httpClientFactory.CreateClient();

            // DefaultController'daki gibi token'ı al ve ekle
            var token = User.Claims.FirstOrDefault(x => x.Type == "carbooktoken")?.Value;
            if (!string.IsNullOrWhiteSpace(token))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var responseMessage = await client.GetAsync("https://localhost:7268/api/Locations");

            if (!responseMessage.IsSuccessStatusCode)
            {
                // 401 ise login'e yönlendir (uygun sayfanı koy)
                if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                    return RedirectToAction("Login", "Account");

                // Başka hata ise boş liste göster (sayfa en azından açılır)
                ViewBag.v = new List<SelectListItem>();
                return View();
            }

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultLocationDto>>(jsonData) ?? new List<ResultLocationDto>();

            var values2 = values.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.LocationID.ToString()
            }).ToList();

            ViewBag.v = values2;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateReservationDto createReservationDto)
        {
            var client = _httpClientFactory.CreateClient();

            // POST da API'ye gidiyor; JWT burada da gerekli
            var token = User.Claims.FirstOrDefault(x => x.Type == "carbooktoken")?.Value;
            if (!string.IsNullOrWhiteSpace(token))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var jsonData = JsonConvert.SerializeObject(createReservationDto);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync("https://localhost:7268/api/Reservations", stringContent);

            if (responseMessage.IsSuccessStatusCode)
                return RedirectToAction("Index", "Default");

            // İstersen ModelState'e hata koyup View'a dön
            ModelState.AddModelError(string.Empty, "Rezervasyon oluşturulamadı.");
            return View();
        }
    }
}
