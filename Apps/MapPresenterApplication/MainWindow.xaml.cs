using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using MapPresenterApplication.Configurations;
using MapPresenterApplication.TempAppLogic.MissionMap;
using Microsoft.Extensions.Logging;

namespace MapPresenterApplication;

public partial class MainWindow : Window
{
    private readonly IMissionMapService _missionMapHandler;

    public MainWindow(IMissionMapService missionMapHandler)
    {
        _missionMapHandler = missionMapHandler;
        InitializeComponent();
        DataContext = this;
    }

    private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        var result = await _missionMapHandler.GetMissionMapAsync();
      
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