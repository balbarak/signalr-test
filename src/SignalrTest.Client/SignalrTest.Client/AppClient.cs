using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignalrTest.Client
{
    public class AppClient
    {
        public const string URL = "";

        private HubConnection _connection;

        public AppClient()
        {

        }

        private void SetupClient()
        {
            _connection = new HubConnectionBuilder()
                .ConfigureLogging((logger) =>
                {
                   
                })
                .WithUrl(URL, config =>
                {
                    config.Transports = HttpTransportType.WebSockets;
                })
                //.AddMessagePackProtocol()
                .Build();

        }

    }
}
