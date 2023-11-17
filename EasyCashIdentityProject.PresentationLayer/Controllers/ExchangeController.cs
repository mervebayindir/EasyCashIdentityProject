using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace EasyCashIdentityProject.PresentationLayer.Controllers
{
    public class ExchangeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            #region Dolar
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://currency-exchange.p.rapidapi.com/exchange?from=USD&to=TRY&q=1.0"),
                Headers =
    {
        { "X-RapidAPI-Key", "512cb6e2bcmshf61f79f728669e4p178a65jsn360324326fb2" },
        { "X-RapidAPI-Host", "currency-exchange.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                if (decimal.TryParse(body, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal usdToTryDecimal))
                {
                    decimal roundedValue = Math.Round(usdToTryDecimal, 4);
                    ViewBag.UsdToTry = roundedValue.ToString("0.0000");
                }
            }
            #endregion

            #region Euro
            var client2 = new HttpClient();
            var request2 = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://currency-exchange.p.rapidapi.com/exchange?from=EUR&to=TRY&q=1.0"),
                Headers =
    {
        { "X-RapidAPI-Key", "512cb6e2bcmshf61f79f728669e4p178a65jsn360324326fb2" },
        { "X-RapidAPI-Host", "currency-exchange.p.rapidapi.com" },
    },
            };
            using (var response2 = await client2.SendAsync(request2))
            {
                response2.EnsureSuccessStatusCode();
                var body2 = await response2.Content.ReadAsStringAsync();
                if (decimal.TryParse(body2, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal eurToTryDecimal))
                {
                    decimal roundedValue2 = Math.Round(eurToTryDecimal, 4);
                    ViewBag.EurToTry = roundedValue2.ToString("0.0000");
                }
            }
            #endregion

            #region Sterlin
            var client3 = new HttpClient();
            var request3 = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://currency-exchange.p.rapidapi.com/exchange?from=GBP&to=TRY&q=1.0"),
                Headers =
    {
        { "X-RapidAPI-Key", "512cb6e2bcmshf61f79f728669e4p178a65jsn360324326fb2" },
        { "X-RapidAPI-Host", "currency-exchange.p.rapidapi.com" },
    },
            };
            using (var response3 = await client3.SendAsync(request3))
            {
                response3.EnsureSuccessStatusCode();
                var body3 = await response3.Content.ReadAsStringAsync();
                if (decimal.TryParse(body3, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal pouToTryDecimal))
                {
                    decimal roundedValue3 = Math.Round(pouToTryDecimal, 4);
                    ViewBag.PouToTry = roundedValue3.ToString("0.0000");
                }
            }
            #endregion

            #region dolar => Euro 
            var client4 = new HttpClient();
            var request4 = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://currency-exchange.p.rapidapi.com/exchange?from=USD&to=EUR&q=1.0"),
                Headers =
    {
        { "X-RapidAPI-Key", "512cb6e2bcmshf61f79f728669e4p178a65jsn360324326fb2" },
        { "X-RapidAPI-Host", "currency-exchange.p.rapidapi.com" },
    },
            };
            using (var response4= await client4.SendAsync(request4))
            {
                response4.EnsureSuccessStatusCode();
                var body4 = await response4.Content.ReadAsStringAsync();
                if (decimal.TryParse(body4, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal usdToEurDecimal))
                {
                    decimal roundedValue4 = Math.Round(usdToEurDecimal, 4);
                    ViewBag.UsdToEur = roundedValue4.ToString("0.0000");
                }
            }

            #endregion

            return View();
        }
    }
}
