using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Werwolf.Hub;

namespace Werwolf.Window;

public partial class MainWindow : System.Windows.Window
{
    public static readonly DependencyProperty MainViewModelProperty = DependencyProperty.Register(
        nameof(MainViewModel), typeof(MainViewModel), typeof(MainWindow), new PropertyMetadata(default(MainViewModel)));

    public MainViewModel MainViewModel
    {
        get => (MainViewModel)GetValue(MainViewModelProperty);
        set => SetValue(MainViewModelProperty, value);
    }

    public MainWindow()
    {
        MainViewModel = new MainViewModel();
        InitializeComponent();
        var service =  new HubService(MainViewModel, MainViewModel.MultiplayerMenuViewModel);
    }

    private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
        {
            DragMove();
        }
    }

    private void CloseProgram_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }
    private void FullScreenProgram_Click(object sender, RoutedEventArgs e)
    {
        if (WindowState == WindowState.Normal)
        {
            WindowState = WindowState.Maximized;
            Background = new SolidColorBrush(Color.FromRgb(0x1f, 0x1f, 0x1f));
            MainViewModel.ScreenModeSymbol = "\u2750";
            Topmost = true;
        }
        else
        {
            WindowState = WindowState.Normal;
            Background = Brushes.Transparent;
            MainViewModel.ScreenModeSymbol = "\u25a2";
            Topmost = true;
        }
    }
}