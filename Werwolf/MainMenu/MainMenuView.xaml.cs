using System.Windows;
using System.Windows.Controls;

namespace Werwolf.MainMenu;

public partial class MainMenuView : UserControl
{
    public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
        nameof(ViewModel), typeof(MainMenuViewModel), typeof(MainMenuView), new PropertyMetadata(default(MainMenuViewModel)));

    public MainMenuViewModel ViewModel
    {
        get { return (MainMenuViewModel)GetValue(ViewModelProperty); }
        set { SetValue(ViewModelProperty, value); }
    }

    public MainMenuView()
    {
        InitializeComponent();
    }
}