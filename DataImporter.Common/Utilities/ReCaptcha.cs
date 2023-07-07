using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Common.Utilities
{
    public class ReCaptcha
    {
        private readonly HttpClient _captchaClient;
        private ILogger<ReCaptcha> _logger;

        public string Secret = "<6LcjBVwcAAAAACh_LtFNsK97E5JgQApuhd3ZMn6Z>";

        public ReCaptcha(HttpClient captchaClient, ILogger<ReCaptcha> logger)
        {
            _captchaClient = captchaClient;
            _logger = logger;
        }

        public async Task<bool> IsValid(string captcha)
        {
            try
            {
                var postTask = await _captchaClient
                    .PostAsync($"?secret=6LcjBVwcAAAAACh_LtFNsK97E5JgQApuhd3ZMn6Z&response={captcha}", new StringContent(""));
                var result = await postTask.Content.ReadAsStringAsync();
                var resultObject = JObject.Parse(result);
                dynamic success = resultObject["success"];
                return (bool)success;
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to process captcha validation", e);
            }
            return false;

        }
    }
}
