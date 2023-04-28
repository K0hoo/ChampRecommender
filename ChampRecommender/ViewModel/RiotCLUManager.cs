using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChampRecommender.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChampRecommender.ViewModel
{
    static class RiotCLUManager
    {

        static public HttpClient httpClient = null;

        static public async Task<JObject> UsingApiEventJObject(string method, string endpoint, object data=null)
        {
            HttpResponseMessage response = await UsingApiEventHttpMessage(method, endpoint, data);
            JObject responseJson = JObject.Parse(await response.Content.ReadAsStringAsync());
            return responseJson;
        }

        static private async Task<HttpResponseMessage> UsingApiEventHttpMessage(string method, string endpoint, object data=null)
        {
            var json = data == null ? "" : JsonConvert.SerializeObject(data);

            switch (method)
            {
                case "Get":
                    return await httpClient.GetAsync(endpoint);
                case "Post":
                    return await httpClient.PostAsync(endpoint, new StringContent(json, Encoding.UTF8, "application/json"));
                case "Put":
                    return await httpClient.PutAsync(endpoint, new StringContent(json, Encoding.UTF8, "application/json"));
                case "Delete":
                    return await httpClient.DeleteAsync(endpoint);
                default:
                    throw new Exception("Unsupported Http method.");
            }
        }
    }
}
