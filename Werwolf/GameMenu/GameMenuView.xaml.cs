using System.Windows;
using System.Windows.Controls;

namespace Werwolf.GameMenu;

public partial class GameMenuView : UserControl
{
    public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
        nameof(ViewModel), typeof(GameMenuViewModel), typeof(GameMenuView), new PropertyMetadata(default(GameMenuViewModel)));

    public GameMenuViewModel ViewModel
    {
        get { return (GameMenuViewModel)GetValue(ViewModelProperty); }
        set { SetValue(ViewModelProperty, value); }
    }
    public GameMenuView()
    {
        InitializeComponent();
    }
}