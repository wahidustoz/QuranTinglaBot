using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QuranTinglaBot.Dto;

namespace QuranTinglaBot.ApiClients
{
    public class OyatClient
    {
        private const string API_BASE_URL = "http://api.alquran.cloud/";
        private readonly ILogger<OyatClient> mLogger;
        private readonly HttpClient mClient;
        private readonly Uri mBaseUri;

        public OyatClient(
            ILogger<OyatClient> logger,
            HttpClient client)
        {
            mLogger = logger;
            mClient = client;
            mBaseUri = new Uri(API_BASE_URL);
        }

        public async Task<OyatEditionsResult> GetOyatEditions()
        {
            var result = new OyatEditionsResult() { IsSuccess = false };
            var uri = new Uri(mBaseUri, "edition/format/audio");

            using(HttpResponseMessage response = await mClient.GetAsync(uri))
            {
                if(!response.IsSuccessStatusCode)
                {
                    mLogger.LogWarning($"Error response at {nameof(OyatClient)} from {uri.AbsolutePath}.\nCode: {response.StatusCode}\nMessage: {response.ReasonPhrase}");
                    result.ErrorMessage = response.ReasonPhrase;
                    result.StatusCode = (int)response.StatusCode;

                    return result;
                }

                string responseString = await response.Content.ReadAsStringAsync();
                if(string.IsNullOrEmpty(responseString))
                {
                    string errorMessage = $"Http Request at {nameof(OyatClient)} from {uri.AbsolutePath} returned empty result.";
                    mLogger.LogWarning(errorMessage);
                    result.ErrorMessage = errorMessage;
                    result.StatusCode = 0;

                    return result;
                }

                OyatEditionsResponse responseObject = JsonConvert.DeserializeObject<OyatEditionsResponse>(responseString);

                result.StatusCode = 200;
                result.Editions = responseObject.Editions;
                result.IsSuccess = true;

                return result;
            }

        }
    }
}