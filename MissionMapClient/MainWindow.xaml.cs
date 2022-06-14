using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.AspNetCore.SignalR.Client;
using MissionMapClient.Configurations;
using Newtonsoft.Json;

namespace MissionMapClient
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

            //    var v= JsonConvert.DeserializeObject<Settings>(System.IO.File.ReadAllText("appsettings.json"));
            var settings = new Settings
            {
                Url = "http://localhost:50003/wsnotificationservice",
                MissionMapNameMethod = "MissionMapUpdated",
                MapEntitiesNameMethod = "MapEntityUpdated"
            };
            _connection = new HubConnectionBuilder()
                .WithUrl(settings.Url)
                .WithAutomaticReconnect()
                .Build();

            _connection.Reconnecting += ConnectionOnReconnecting;
            _connection.Reconnected += ConnectionOnReconnected;
            _connection.Closed += ConnectionOnClosed;
            _connection.On<string>(settings.MissionMapNameMethod, GetMissionMapMessage);
            _connection.On<string>(settings.MapEntitiesNameMethod, GetMapEntityMessage);

            try
            {
                Connect();
                Messages.Items.Add("Connection started");
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
                await _connection.StartAsync();
            }
            catch (Exception e)
            {
                Messages.Items.Add(e.Message);
            }
        }
    }
}
