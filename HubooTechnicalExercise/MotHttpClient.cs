using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace HubooTechnicalExercise
{
    public class MotHttpClient
    {
        private const string url = "https://beta.check-mot.service.gov.uk/trade/vehicles/mot-tests?registration={0}";
        private readonly HttpClient _httpClient;

        public MotHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;

            //This should be in a environment store such as appsettings and then substituted in from the pipeline
            //or using a cloud credential client to manage secrets for you
            if (!_httpClient.DefaultRequestHeaders.Contains("x-api-key"))
            {
                _httpClient.DefaultRequestHeaders.Add("x-api-key", "<redacted>");
            }
        }

        public async Task<(Car, HttpStatusCode)> GetMotHistoryAsync(string regNumber)
        {
            if (string.IsNullOrWhiteSpace(regNumber))
            {
                return (null, HttpStatusCode.NotFound);
            }

            //At this point before making the request, if I had the time, I would check a cache that
            //would be valid for 1 day. This would reduce the calls to the external API (so is
            //better for quota usage) and decrease response time

            HttpResponseMessage response;

            try
            {
                response = await _httpClient.GetAsync(string.Format(url, regNumber));
            }
            //In an API I would just let this error
            catch (HttpRequestException e)
            {
                Console.WriteLine("Oops! Something went wrong!");
                Console.WriteLine(e.Message);
                return (null, HttpStatusCode.InternalServerError);
            }

            if (!response.IsSuccessStatusCode)
            {
                return (null, response.StatusCode);
            }

            var motResponseData = JsonConvert.DeserializeObject<IEnumerable<Car>>(await response.Content.ReadAsStringAsync());

            return (motResponseData.First(), response.StatusCode);
        }
    }
}
