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
using System.Diagnostics;
using OpenQA.Selenium;

namespace ChampRecommender.ViewModel
{
    static class RiotCLUManager
    {

        static public HttpClient? httpClient = null;

        static public async Task<JObject>? UsingApiEventJObject(string method, string endpoint, object? data=null)
        {
            try
            {
                
                HttpResponseMessage response = await UsingApiEventHttpMessage(method, endpoint, data);
                JObject responseJson = JObject.Parse(await response.Content.ReadAsStringAsync());
                return responseJson;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        static public async Task<JArray>? UsingApiEventJArray(string method, string endpoint, object? data=null)
        {
            try
            {
                JArray responseJsonArray;
                HttpResponseMessage response = await UsingApiEventHttpMessage(method, endpoint, data);
                string responseContent = await response.Content.ReadAsStringAsync();
                if (!responseContent.StartsWith('{') && !responseContent.StartsWith('[')) {
                    responseJsonArray = JArray.Parse(String.Format("[{0}]", responseContent));
                }
                else
                {
                    responseJsonArray = JArray.Parse(await response.Content.ReadAsStringAsync());
                }
                return responseJsonArray;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        static private async Task<HttpResponseMessage> UsingApiEventHttpMessage(string method, string endpoint, object? data=null)
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

    static class ServerManager
    {
        static public HttpClient? httpServer = null;
        static public HttpClient? httpResultClient = null;

        public static async Task<JArray> getRecommendation(object? data)
        {
            var json = data == null ? "" : JsonConvert.SerializeObject(data);

            HttpResponseMessage response = await httpServer.PostAsync("", new StringContent(json, Encoding.UTF8, "application/json"));
            JArray responseJson = JArray.Parse(await response.Content.ReadAsStringAsync());
            return responseJson;
        }

        public static async Task<bool> sendGameResult(object? data)
        {
            try
            {
                var json = data == null ? "" : JsonConvert.SerializeObject(data);

                HttpResponseMessage response = await httpResultClient.PostAsync("", new StringContent(json, Encoding.UTF8, "application/json"));
                return true;
            }
            catch 
            { 
                return false;
            }
        }
    }
}
