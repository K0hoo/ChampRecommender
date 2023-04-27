using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using System.Diagnostics;
using ChampRecommender.Models;
using System.Net.Http.Headers;
using System.Windows.Input;

namespace ChampRecommender.ViewModel
{
    internal class ClientViewModel : BaseViewModel
    {
        public delegate void LeagueClosedHandler();
        public event LeagueClosedHandler LeagueClosed;
        private HttpClient httpClient = null;
        
        public ClientViewModel() 
        {
        }

        public void Connect()
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

                RiotCLUManager.httpClient = httpClient;
            }
            catch 
            {
                Console.WriteLine("Connect Failed");
            }
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

        public bool CheckClientOn()
        {
            Process[] processes = Process.GetProcessesByName(ClientData.CLIENT_NAME);

            if (processes.Length != 0) {
                ClientData.clientProcess = processes[0];
                return true;
            } else { return false; }
        }

        
        private void ClientProcess_Exited(object sender, EventArgs e)
        {
            LeagueClosed();
        }
    }
}
