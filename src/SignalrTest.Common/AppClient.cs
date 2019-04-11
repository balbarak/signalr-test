using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SignalrTest.Common
{
    public class AppClient
    {
        public event EventHandler<ProtocolGroupMessage> OnGroupMessage;
        public event EventHandler<Exception> OnDisconnected;
        public event EventHandler OnConnected;

        //public const string URL = "http://192.168.100.24:5000/hub";

        public const string URL = "http://172.20.10.13:5000/hub";

        public const string GROUP_ID = "008406cd-5dd5-4d10-a43f-2dae33a88e33";

        private HubConnection _connection;
        
        public bool IsConnected { get; private set; }

        public bool IsConnecting { get; private set; }
        
        public AppClient()
        {
            SetupClient();
        }

        public async Task Connect()
        {
            try
            {
                IsConnecting = true;

                await _connection.StartAsync();

                IsConnected = true;

                IsConnecting = false;

                OnConnected?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                await OnConnectionClosed(ex);
            }
            finally
            {

            }
        }

        public async Task Disconnect()
        {
            try
            {
                await OnConnectionClosed(null);

                await _connection.StopAsync();

            }
            finally
            {
                IsConnected = false;
                IsConnecting = false;
            }
        }

        public Task SendGroupMessage(ProtocolGroupMessage msg)
        {
            return _connection.InvokeAsync<bool>("SendGroupMessage", msg);
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
                .Add
                .Build();

            _connection.Closed += OnConnectionClosed;
            _connection.On<ProtocolGroupMessage>("OnGroupMessage", (args) => OnGroupMessageInternal(args));

        }
        
        private Task OnConnectionClosed(Exception e)
        {
            IsConnected = false;

            IsConnecting = false;

            OnDisconnected?.Invoke(this, e);

            return Task.CompletedTask;
        }

        private void OnGroupMessageInternal(ProtocolGroupMessage e)
        {
            OnGroupMessage?.Invoke(this, e);
        }
    }
}
