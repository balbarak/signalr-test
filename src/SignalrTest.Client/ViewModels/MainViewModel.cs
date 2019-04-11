using SignalrTest.Client.Helpers;
using SignalrTest.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SignalrTest.Client.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private AppClient _client = new AppClient();
        
        public bool IsConnected => _client.IsConnected;

        public bool IsDisconnected => !_client.IsConnected;

        private string _status = "Ready";

        public string Status
        {
            get { return _status; }
            set { _status = value; OnPropertyChanged(); }
        }
        
        public ICommand ConnectCommand => new AsyncCommand(Connect);

        public ICommand DisconnectCommand => new AsyncCommand(Disconnect);

        public ObservableCollection<ProtocolGroupMessage> Messages { get; private set; } = new ObservableCollection<ProtocolGroupMessage>();

        public MainViewModel()
        {
            _client.OnDisconnected += OnDisconnected;
            _client.OnConnected += OnConnected;
            _client.OnGroupMessage += OnGroupMessages;
        }

        public async Task Connect()
        {
            Status = $"Connecting to {AppClient.URL} ...";

            await _client.Connect();

            NotifyConnectionsStatus();
        }

        public async Task Disconnect()
        {
            Status = $"Disconnecting ...";

            await _client.Disconnect();

            NotifyConnectionsStatus();
        }

        private void NotifyConnectionsStatus()
        {
            OnPropertyChanged(nameof(IsConnected));
            OnPropertyChanged(nameof(IsDisconnected));
        }

        private void OnConnected(object sender, EventArgs e)
        {
            Status = "Connected";
        }

        private void OnDisconnected(object sender, Exception e)
        {
            NotifyConnectionsStatus();

            Status = "Disconnected";
        }


        private void OnGroupMessages(object sender, ProtocolGroupMessage e)
        {
            Device.BeginInvokeOnMainThread(() => Messages.Add(e));
        }


        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
