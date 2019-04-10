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

namespace SignalrTest.Client.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private AppClient _client = new AppClient();
        private ObservableCollection<ProtocolGroupMessage> _messages;

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

        public ObservableCollection<ProtocolGroupMessage> Messages
        {
            get { return _messages; }
            set { _messages = value; OnPropertyChanged(); }
        }
        
        public MainViewModel()
        {
            _client.OnDisconnected += OnDisconnected;
            _client.OnConnected += OnConnected;
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
