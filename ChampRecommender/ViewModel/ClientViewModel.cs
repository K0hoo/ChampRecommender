﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using System.Diagnostics;
using ChampRecommender.Models;
using System.Net.Http.Headers;
using System.Windows.Input;
using System.IO;
using ChampRecommender.Dataset;

namespace ChampRecommender.ViewModel
{
    internal class ClientViewModel : BaseViewModel
    {
        public delegate void LeagueClosedHandler();
        public event LeagueClosedHandler LeagueClosed;
        private HttpClient? httpClient = null;
        private HttpClient? httpServer = null;
        private HttpClient? httpResultServer = null;
        
        public ClientViewModel() 
        {
            Champions.initChampions();
        }

        public async Task Connect()
        {
            try
            {
                ClientData.LeaguePath = Path.GetDirectoryName(ClientData.clientProcess.MainModule.FileName);
                var lockFilePath = Path.Combine(ClientData.LeaguePath, "lockfile");

                using (var fileStream =  new FileStream(lockFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var reader = new StreamReader(fileStream))
                {
                    var text = reader.ReadToEnd();
                    string[] items = text.Split(':');
                    ClientData.ToKen = items[3];
                    ClientData.Port = ushort.Parse(items[2]);
                    ClientData.ApiUrl = "https://127.0.0.1:" + ClientData.Port.ToString() + "/";
                }

                ClientConnectInit();

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                                            "Basic"
                                            , Convert.ToBase64String(Encoding.ASCII.GetBytes($"riot:{ClientData.ToKen}")));
                httpClient.BaseAddress = new Uri(ClientData.ApiUrl);

                ClientData.clientProcess.EnableRaisingEvents = true;
                ClientData.clientProcess.Exited += ClientProcess_Exited;

                RiotCLUManager.httpClient = httpClient;

                ServerConnectInit();

                httpServer.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpServer.BaseAddress = new Uri(ServerData.ServerUrl);

                ServerManager.httpServer = httpServer;

                httpResultServer.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpResultServer.BaseAddress = new Uri(ServerData.ServerResultUrl);

                ServerManager.httpResultClient = httpResultServer;
            }
            catch 
            {
                Console.WriteLine("Connect Failed");
            }
        }

        private void ClientConnectInit()
        {
            this.httpClient = null;
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (HttpResponseMessage, cert, cetChain, policyErrors) => { return true; };
            httpClient = new HttpClient(handler);
        }

        public void ServerConnectInit()
        {
            this.httpServer = null;
            this.httpResultServer = null;
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions= ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (HttpResponseMessage, cert, cetChain, policyErrors) => { return true; };
            httpServer = new HttpClient(handler);
            handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (HttpResponseMessage, cert, cetChain, policyErrors) => { return true; };
            httpResultServer = new HttpClient(handler);
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
