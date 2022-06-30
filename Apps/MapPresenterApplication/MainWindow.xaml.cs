using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MapPresenterApplication.Configurations;
using MapPresenterApplication.Model;
using MapPresenterApplication.TempAppLogic.MissionMap;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MapPresenterApplication;

public partial class MainWindow : Window
{
    private readonly ILogger<MainWindow> _logger;
    private readonly IMissionMapService _missionMapService;
    private readonly HubConnection _connection;
    private readonly int _reconnectInterval = 2000;

    public MainWindow(
        Settings settings,
        ILogger<MainWindow> logger,
        IMissionMapService missionMapService)
    {
        _logger = logger;
        _missionMapService = missionMapService;
        InitializeComponent();
        DataContext = this;

        LoadMissionMap();

        _connection = new HubConnectionBuilder()
            .WithUrl(settings.HubSettings!.Url!)
            .WithAutomaticReconnect()
            .Build();

        _connection.Reconnecting += OnConnectionReconnecting;
        _connection.Reconnected += OnConnectionReconnected;
        _connection.Closed += OnConnectionClosed;
        _connection.On<string>(settings.HubSettings!.MissionMapNameMethod!, OnMissionMapChanged);
        _connection.On<string>(settings.HubSettings!.MapEntitiesNameMethod!, OnMapEntityPublished);

        ConnectToHub();
    }

    private void OnMapEntityPublished(string obj)
    {
        _logger.LogInformation("Map entity publish: {publishedMapEntity}", obj);
        var entity = JsonConvert.DeserializeObject<MapEntityModel>(obj);
        if (entity != null)
        {
            OnEntityPublished(entity);
        }
    }
    private void OnEntityPublished(MapEntityModel entity)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            var shapeEllipse = new Ellipse
            {
                Fill = Brushes.Purple,
                Stroke = Brushes.MediumPurple,
                Width = 10,
                Height = 10,
                StrokeThickness = 2
            };

            var titleTextBox = new TextBlock
            {
                Foreground = Brushes.Black,
                FontSize = 8,
                Text = entity.Title
            };

            var entityStackPanel = new StackPanel();
            entityStackPanel.Children.Add(shapeEllipse);
            entityStackPanel.Children.Add(titleTextBox);

            (double top, double left) = (entity.Lat, entity.Lon);
            EntitiesShowerCanvas.Children.Add((entityStackPanel));
            Canvas.SetTop(entityStackPanel, top);
            Canvas.SetLeft(entityStackPanel, left);
        });
    }

    private Task OnMissionMapChanged(string arg)
    {
        _logger.LogInformation("Mission ap updated: {missionMapName}", arg);
        LoadMissionMap();
        return Task.CompletedTask;
    }

    private async void ConnectToHub()
    {
        var continueToReconnect = true;
        do
        {
            try
            {
                _logger.LogInformation("Connecting to Hub...");
                await _connection.StartAsync();
                continueToReconnect = false;
                _logger.LogInformation("Connected to Hub");
            }
            catch (Exception e)
            {
                _logger.LogInformation(
                    e,
                    "Connection attempt failed, error: {errorMessage}. Reconnection attempt in {reconnectInterval} seconds.",
                    e.Message,
                    _reconnectInterval / 1000);
                Thread.Sleep(_reconnectInterval);
            }
        } while (continueToReconnect);
    }

    private Task OnConnectionClosed(Exception? ex)
    {
        _logger.LogInformation(
            ex,
            "Hub connection closed, error: {errorMessage}",
            ex?.Message ?? "No exception args");

        ConnectToHub();

        return Task.CompletedTask;
    }

    private Task OnConnectionReconnected(string? arg)
    {
        _logger.LogInformation(
            "Hub connection reconnected, arg: {arg}",
            arg ?? "No arg");

        return Task.CompletedTask;
    }

    private Task OnConnectionReconnecting(Exception? ex)
    {
        _logger.LogInformation(
            ex,
            "Hub connection reconnecting, error: {errorMessage}",
            ex?.Message ?? "No exception args");

        return Task.CompletedTask;
    }

    private async void LoadMissionMap()
    {
        await OnMissionMapChangedAsync();
    }

    private async Task OnMissionMapChangedAsync()
    {
        var result = await _missionMapService.GetMissionMapAsync();

        var binaryData = Convert.FromBase64String(result!.MapBase64);
        var bi = new BitmapImage();
        bi.BeginInit();
        bi.StreamSource = new MemoryStream(binaryData);
        bi.EndInit();
        MissionMapImage.Source = bi;

        var mapName = result.MapName.Split(".")[0];
        Title = mapName;
        Icon = bi;
    }
}