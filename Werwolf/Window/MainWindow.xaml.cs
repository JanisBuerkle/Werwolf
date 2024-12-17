using System.Windows;

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
        InitializeComponent();
    }
}