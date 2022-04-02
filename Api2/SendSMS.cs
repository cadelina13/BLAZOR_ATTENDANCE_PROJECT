using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using SharedClass;

namespace Api2
{
    public static class Function1
    {
        private static readonly string apiKey = "1vI6Ri0Ek3oAriAOlmKakibB9Lb";
        private static readonly string apiSecret = "LQpj7KEpoyaUgHjqw8xuZ43sgxAF3y14Kuh9nF0t";

        [FunctionName("SendSMS")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var result = JsonConvert.DeserializeObject<SMSModel>(requestBody);


            var client = new RestClient("https://api.movider.co/v1/sms");

            var request = new RestRequest("", Method.Post);
            request.RequestFormat = DataFormat.None;
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            string data = $"api_key={apiKey}&api_secret={apiSecret}&to={result.To}&text={result.Text}";
            request.AddBody(data, "application/x-www-form-urlencoded");
            var response = client.ExecuteAsync(request).Result;
            //Console.WriteLine(response.Content.ToString());

            return new OkObjectResult(response.Content.ToString());
        }
    }
}
