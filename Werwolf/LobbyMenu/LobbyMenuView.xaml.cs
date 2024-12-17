using System.Windows;
using System.Windows.Controls;

namespace Werwolf.LobbyMenu;

public partial class LobbyMenuView : UserControl
{
    public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
        nameof(ViewModel), typeof(LobbyMenuViewModel), typeof(LobbyMenuView), new PropertyMetadata(default(LobbyMenuViewModel)));

    public LobbyMenuViewModel ViewModel
    {
        get { return (LobbyMenuViewModel)GetValue(ViewModelProperty); }
        set { SetValue(ViewModelProperty, value); }
    }
    public LobbyMenuView()
    {
        InitializeComponent();
    }
}