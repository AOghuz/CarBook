using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using CarBook.Dto.ReviewDtos;

namespace CarBook.WebUI.ViewComponents.CarDetailViewComponents
{
    public class _CarDetailCommentsByCarIdComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public _CarDetailCommentsByCarIdComponentPartial(IHttpClientFactory httpClientFactory)
            => _httpClientFactory = httpClientFactory;

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            ViewBag.carid = id;

            var client = _httpClientFactory.CreateClient();
            var resp = await client.GetAsync($"https://localhost:7268/api/Reviews/car/{id}");

            if (!resp.IsSuccessStatusCode)
                return View("Default", new List<ResultReviewByCarIdDto>()); // boş liste dön

            var json = await resp.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultReviewByCarIdDto>>(json)
                         ?? new List<ResultReviewByCarIdDto>();

            return View("Default", values);
        }
    }
}
