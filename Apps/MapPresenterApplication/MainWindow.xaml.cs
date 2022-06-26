using System.Windows;
using MapPresenterApplication.Configurations;
using Microsoft.Extensions.Logging;

namespace MapPresenterApplication;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;
    }
}