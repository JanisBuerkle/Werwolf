using System.Windows;
using System.Windows.Controls;

namespace Werwolf.MultiplayerMenu;

public partial class MultiplayerMenuView : UserControl
{
    public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
        nameof(ViewModel), typeof(MultiplayerMenuViewModel), typeof(MultiplayerMenuView), new PropertyMetadata(default(MultiplayerMenuViewModel)));

    public MultiplayerMenuViewModel ViewModel
    {
        get { return (MultiplayerMenuViewModel)GetValue(ViewModelProperty); }
        set { SetValue(ViewModelProperty, value); }
    }
    public MultiplayerMenuView()
    {
        InitializeComponent();
    }
}