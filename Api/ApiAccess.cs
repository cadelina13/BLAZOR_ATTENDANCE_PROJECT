using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using SharedClass;

namespace Api
{
    public static class ApiAccess
    {
        private static readonly string apiKey = "1vI6Ri0Ek3oAriAOlmKakibB9Lb";
        private static readonly string apiSecret = "LQpj7KEpoyaUgHjqw8xuZ43sgxAF3y14Kuh9nF0t";
        [Function("SendSMS")]
        public static async Task<IActionResult> SendSMS([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var tasks = new List<Task>();
            var result = JsonConvert.DeserializeObject<List<SMSModel>>(requestBody);
            foreach (var i in result)
            {
                tasks.Add(Task.Run(() =>
                {
                    var client = new RestClient("https://api.movider.co/v1/sms");
                    var request = new RestRequest("", Method.Post);
                    request.RequestFormat = DataFormat.None;
                    request.AddHeader("Accept", "application/json");
                    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    string data = $"api_key={apiKey}&api_secret={apiSecret}&to={i.To}&text={i.Text}";
                    request.AddBody(data, "application/x-www-form-urlencoded");
                    client.ExecuteAsync(request);
                }));
            }
            await Task.WhenAll(tasks);
            return new OkObjectResult(true);
        }
    }
}
