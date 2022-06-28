using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.AspNetCore.SignalR.Client;
using MissionMapClient.Configurations;
using Newtonsoft.Json;

namespace NotificationServiceTester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly HubConnection _connection;

        public MainWindow()
        {
            InitializeComponent();

            var settings = JsonConvert.DeserializeObject<Settings>(System.IO.File.ReadAllText("appsettings.json"));

            if (settings == null) 
                return;

            _connection = new HubConnectionBuilder()
                .WithUrl(settings.MissionMapHubSettings.Url)
                .WithAutomaticReconnect()
                .Build();

            _connection.Reconnecting += ConnectionOnReconnecting;
            _connection.Reconnected += ConnectionOnReconnected;
            _connection.Closed += ConnectionOnClosed;
            _connection.On<string>(settings.MissionMapHubSettings.MissionMapNameMethod, GetMissionMapMessage);
            _connection.On<string>(settings.MissionMapHubSettings.MapEntitiesNameMethod, GetMapEntityMessage);

            try
            {
                Connect();
            }
            catch (Exception e)
            {
                Messages.Items.Add(e.Message);
            }
        }

        private Task ConnectionOnReconnecting(Exception? arg)
        {
            Dispatcher.Invoke(() => { Messages.Items.Add("Reconnecting..."); });
            return Task.CompletedTask;
        }

        private Task ConnectionOnReconnected(string? arg)
        {
            Dispatcher.Invoke(() => { Messages.Items.Add("Reconnected"); });
            return Task.CompletedTask;
        }

        private Task ConnectionOnClosed(Exception? arg)
        {
            Dispatcher.Invoke(() => { Messages.Items.Add("Connection closed"); });
            return Task.CompletedTask;
        }

        private void GetMissionMapMessage(string missionMapName)
        {
            Dispatcher.Invoke(() => { Messages.Items.Add($"Mission map changed to {missionMapName}"); });
        }

        private void GetMapEntityMessage(string entityMap)
        {
            Dispatcher.Invoke(() => { Messages.Items.Add($"Entity: {entityMap}"); });
        }

        private async void Connect()
        {
            try
            {
                Messages.Items.Add("Connecting...");
                await _connection.StartAsync();
                Messages.Items.Add("Connected");
            }
            catch (Exception e)
            {
                Messages.Items.Add(e.Message);
                Messages.Items.Add("Connection failed. Attempting to reconnect...");
                Thread.Sleep(2000);
                Connect();
            }
        }
    }
}
