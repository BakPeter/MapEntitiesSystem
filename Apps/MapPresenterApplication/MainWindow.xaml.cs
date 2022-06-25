using System.Windows;
using Microsoft.Extensions.Logging;

namespace MapPresenterApplication;

public partial class MainWindow : Window
{
    private readonly ILogger<MainWindow> _logger;

    public MainWindow(ILogger<MainWindow> logger)
    {
        _logger = logger;
        InitializeComponent();
        DataContext = this;

        _logger.LogInformation(
            "Main window c'tor, number is {number}, text is {text}, bollean flag is {booleanFlag}", 
            11, "this is a text", true);
    }
}