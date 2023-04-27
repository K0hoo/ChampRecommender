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
    class RiotCLUManager
    {
        public delegate void LeagueClosedHandler();
        public event LeagueClosedHandler LeagueClosed;

        private HttpClient httpClient = null;

        public RiotCLUManager() 
        {
        }

        public bool Connect()
        {
            try
            {
                ConnectInit();

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                                            "Basic"
                                            , Convert.ToBase64String(Encoding.ASCII.GetBytes($"riot:{ClientData.ToKen}")));
                httpClient.BaseAddress = new Uri(ClientData.ApiUrl);

                ClientData.clientProcess.EnableRaisingEvents = true;
                ClientData.clientProcess.Exited += ClientProcess_Exited;

                return true;
            }
            catch { }
            return true;
        }

        private void ConnectInit()
        {
            this.httpClient = null;

            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback = 
                (HttpResponseMessage, cert, cetChain, policyErrors) => { return true; };
            httpClient = new HttpClient(handler);
        }

        public async Task<Object> UsingApiEventJObject(string method, string endpoint, object data=null)
        {
            var response = await UsingApiEventHttpMessage(method, endpoint, data);
            var responseJson = JObject.Parse(await response.Content.ReadAsStringAsync());
            return responseJson;
        }

        public async Task<HttpResponseMessage> UsingApiEventHttpMessage(string method, string endpoint, object data=null)
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

        private void ClientProcess_Exited(object sender, EventArgs e)
        {
            LeagueClosed();
        }
    }
}
