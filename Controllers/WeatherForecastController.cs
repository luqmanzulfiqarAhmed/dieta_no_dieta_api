using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using EF_DietaNoDietaApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EF_DietaNoDietaApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/WeatherForecast")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            HttpClient client = new HttpClient();
            var byteArray = Encoding.ASCII.GetBytes("4fdaca5919a5426b85d0e35297a09a0e:33346a1a4fcf4b0e831ece0943127e4b");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            var values = new Dictionary<string, string>
            {
                { "scope", "basic" },
                { "grant_type", "client_credentials" }
            };
            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("https://oauth.fatsecret.com/connect/token", content);
            FatSecretAccessToken responseObject = JsonConvert.DeserializeObject<FatSecretAccessToken>(response.Content.ReadAsStringAsync().Result);

            byteArray = Encoding.ASCII.GetBytes(responseObject.access_token);
            var requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                Content = new StringContent(".....", Encoding.UTF8, "application/json"),
                RequestUri = new Uri("https://platform.fatsecret.com/rest/server.api")
            };

            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", responseObject.access_token);

            response = await client.SendAsync(requestMessage);

            string url = "https://platform.fatsecret.com/rest/server.api"; // Just a sample url
            WebClient wc = new WebClient();
            wc.Headers[HttpRequestHeader.Authorization] = "Bearer " + responseObject.access_token;
            wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
            wc.Headers[HttpRequestHeader.Accept] = "application/json";
            wc.QueryString.Add("method", "foods.search");
            wc.QueryString.Add("format", "json");
            wc.QueryString.Add("search_expression", "french-toast-plain");

            var data = wc.UploadValues(url, "POST", wc.QueryString);

            // data here is optional, in case we recieve any string data back from the POST request.
            var responseString = UnicodeEncoding.UTF8.GetString(data);

            return Ok(responseString);
        }
        
    }
}
